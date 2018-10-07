using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace File.System.Stub
{
	public class AsyncEqualityComparer
	{
		private readonly DirectoryPair EndDirectoryPair = new DirectoryPair(null, null);
		private readonly IRootPathEqualityStrategy _rootPathEqualityStrategy;
		private readonly IDirectoryFileEqualityStrategy _directoryFileEqualityStrategy;
		private ConcurrentQueue<DirectoryPair> _directoriesToCheck = new ConcurrentQueue<DirectoryPair>();

		public AsyncEqualityComparer(IRootPathEqualityStrategy rootPathEqualityStrategy, IDirectoryFileEqualityStrategy directoryLevelEqualityStrategy)
		{
			_rootPathEqualityStrategy = rootPathEqualityStrategy;
			_directoryFileEqualityStrategy = directoryLevelEqualityStrategy;
		}

		public async Task<bool> AreEqualAsync(RootStub first, RootStub second, CancellationToken token)
		{
			if (first == null)
			{
				throw new ArgumentNullException(nameof(first));
			}
			if (second == null)
			{
				throw new ArgumentNullException(nameof(second));
			}
			if(!_rootPathEqualityStrategy.AreEqual(first.Path, second.Path))
			{
				return false;
			}
			_directoriesToCheck.Enqueue(new DirectoryPair(first.Directory, second.Directory));
			return await CheckDirectoryEquality(token).ConfigureAwait(false);
		}

		private async Task<bool> CheckDirectoryEquality(CancellationToken token)
		{
			var taskList = Enumerable.Repeat(0, 4).Select(_ => Dirs(token)).ToList();


			var results = await Task.WhenAll(taskList).ConfigureAwait(false);
			return results.All(result => result);
		}

		private async Task<bool> Dirs(CancellationToken cancellationToken)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				DirectoryPair directoryPair;
				if (_directoriesToCheck.TryDequeue(out directoryPair))
				{
					if(ReferenceEquals(directoryPair, EndDirectoryPair))
					{
						return true;
					}
					if(!AreDirectoriesEqual(directoryPair.First, directoryPair.Second, cancellationToken))
					{
						return false;
					}
				}
				else
				{
					await Task.Delay(10, cancellationToken).ConfigureAwait(false);
				}
			}
			return true;
		}

		private bool AreDirectoriesEqual(DirectoryStub first, DirectoryStub second, CancellationToken token)
		{
			if(!_directoryFileEqualityStrategy.AreEqual(first, second))
			{
				return false;
			}
			if(first.Directories.Count != second.Directories.Count)
			{
				return false;
			}
			List<DirectoryPair> directoryPairs = first.Directories.Join(second.Directories, outerDir => outerDir.Name, innerDir => innerDir.Name, (outerDir, innerDir) => new DirectoryPair(outerDir, innerDir)).ToList();
			if(directoryPairs.Count != first.Directories.Count)
			{
				return false;
			}
			foreach(var directoryPair in directoryPairs)
			{
				_directoriesToCheck.Enqueue(directoryPair);
			}
			return true;
		}
	}
}
