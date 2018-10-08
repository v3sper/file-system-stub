using File.System.Stub.IO;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace File.System.Stub
{

	public sealed class DirectoryStructureReader
	{
		private readonly ConcurrentQueue<IReadOperation> _readOperations = new ConcurrentQueue<IReadOperation>();
		private readonly IDirectoryInformationFactory _directoryInfoFactory;

		public DirectoryStructureReader() : this(new DirectoryInformationFactory())
		{

		}

		public DirectoryStructureReader(IDirectoryInformationFactory directoryInfoFactory)
		{
			_directoryInfoFactory = directoryInfoFactory;
		}

		public RootStub Read(string rootPath)
		{
			if (rootPath == null)
			{
				throw new ArgumentNullException(nameof(rootPath), "Root path cannot be null.");
			}

			var stub = new RootStub
			{
				Path = rootPath, 
				Directory = GetContent(rootPath)
			};
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
