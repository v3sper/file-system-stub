using File.System.Stub.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace File.System.Stub
{

	public sealed class DirectoryStructureReader
	{
		private readonly ConcurrentQueue<IReadOperation> _readOperations = new ConcurrentQueue<IReadOperation>();
		private readonly IDirectoryInformationFactory _directoryInfoFactory;
		private readonly int _maxSimultaneousOperations;

		public DirectoryStructureReader(IDirectoryInformationFactory directoryInfoFactory, /*wywalić*/int maxSimultaneousOperations = 1)
		{
			_directoryInfoFactory = directoryInfoFactory;
			_maxSimultaneousOperations = maxSimultaneousOperations;
		}

		public RootStub Read(string rootPath)
		{
			if (rootPath == null)
			{
				throw new ArgumentNullException(nameof(rootPath), "Root path cannot be null.");
			}
			var stub = new RootStub { Path = rootPath };
			stub.Directory = GetContent(rootPath);
			return stub;
		}

		// TODO najpewniej wywalić :)
		public async Task<RootStub> ReadAsync(string path, CancellationToken token)
		{
			var stub = new RootStub { Path = path };
			stub.Directory = await GetContentAsync(path, token).ConfigureAwait(false);
			return stub;
		}

		private DirectoryStub GetContent(string path)
		{
			IDirectoryInformation directoryInfo = _directoryInfoFactory.Create(path);
			DirectoryStub directoryStub = new DirectoryStub { Name = directoryInfo.Name };
			_readOperations.Enqueue(new ReadDirectoryOperation(directoryStub, directoryInfo));
			while (_readOperations.TryDequeue(out var readOperation))
			{
				DoOperation(readOperation);
			}
			return directoryStub;
		}

		// TODO najpewniej wywalić :)
		private async Task<DirectoryStub> GetContentAsync(string path, CancellationToken token)
		{
			DirectoryStub directoryStub = new DirectoryStub { Name = path };
			_readOperations.Enqueue(new ReadDirectoryOperation(directoryStub, _directoryInfoFactory.Create(path)));
			await Task.WhenAll(Enumerable.Repeat(0, _maxSimultaneousOperations).Select(_ => Task.Run(() => DoRead(new SemaphoreSlim(_maxSimultaneousOperations), token))).ToArray()).ConfigureAwait(false);
			return directoryStub;
		}

		// TODO najpewniej wywalić :)
		private async Task DoRead(SemaphoreSlim semaphore, CancellationToken token)
		{
			while (!token.IsCancellationRequested)
			{
				if (_readOperations.TryDequeue(out var readOperation))
				{
					try
					{
						await semaphore.WaitAsync(token).ConfigureAwait(false);
						DoOperation(readOperation);
					}
					finally
					{
						semaphore.Release();
					}
				}
				else if (semaphore.CurrentCount == _maxSimultaneousOperations)
				{
					return;
				}
				else
				{
					await Task.Delay(1).ConfigureAwait(false);
				}
			}
		}

		private void DoOperation(IReadOperation readOperation)
		{
			IEnumerable<IReadOperation> newOperations = readOperation.DoOperation();
			foreach(var newOperation in newOperations)
			{
				_readOperations.Enqueue(newOperation);
			}
		}
	}
}
