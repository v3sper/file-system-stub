using File.System.Stub.IO;
using System.Collections.Generic;
using System.Linq;

namespace File.System.Stub.IO
{
	public class ReadDirectoryOperation : IReadOperation
	{
		private readonly IDirectoryInfo _directoryInfo;

		public DirectoryStub CurrentStub { get; }
		private readonly IDirectoryInfoFactory _directoryInfoFactory;

		public ReadDirectoryOperation(DirectoryStub directoryStub, IDirectoryInfo directoryInfo, IDirectoryInfoFactory directoryInfoFactory)
		{
			CurrentStub = directoryStub;
			_directoryInfo = directoryInfo;
			_directoryInfoFactory = directoryInfoFactory;
		}

		public IEnumerable<IReadOperation> DoOperation()
		{
			CurrentStub.Files = _directoryInfo.EnumerateFiles().Select(GetFileStubFromFileInfo).ToList();
			var directoryOperationList = _directoryInfo.EnumerateDirectories().Select(dirInfo => new ReadDirectoryOperation(new DirectoryStub { ParentDirectory = CurrentStub, Name = dirInfo.Name }, dirInfo, _directoryInfoFactory)).ToList();
			CurrentStub.Directories = directoryOperationList.Select(dirOperation => dirOperation.CurrentStub).ToList();
			return directoryOperationList;
		}

		private FileStub GetFileStubFromFileInfo(IFileInformation fileInfo)
		{
			return new FileStub
			{
				Name = fileInfo.Name,
				CreationTime = fileInfo.CreationTimeUtc,
				LastWriteTime = fileInfo.LastWriteTimeUtc,
				LastAccessTime = fileInfo.LastAccessTimeUtc,
				IsReadOnly = fileInfo.IsReadOnly,
				ParentDirectory = CurrentStub
			};
		}
	}
}
