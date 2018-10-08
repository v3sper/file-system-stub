using System.Collections.Generic;
using System.Linq;

namespace File.System.Stub.IO
{
	internal sealed class ReadDirectoryOperation : IReadOperation
	{
		private readonly IDirectoryInformation _directoryInfo;

		private readonly DirectoryStub _currentStub;

		public ReadDirectoryOperation(DirectoryStub directoryStub, IDirectoryInformation directoryInfo)
		{
			_currentStub = directoryStub;
			_directoryInfo = directoryInfo;
		}

		public IEnumerable<IReadOperation> DoOperation()
		{
			_currentStub.Files = _directoryInfo.EnumerateFiles().Select(GetFileStubFromFileInfo).ToList();
			var directoryOperationList = _directoryInfo.EnumerateDirectories().Select(CreateReadDirectoryOperation).ToList();
			_currentStub.Directories = directoryOperationList.Select(dirOperation => dirOperation._currentStub).ToList();
			return directoryOperationList;
		}

		private ReadDirectoryOperation CreateReadDirectoryOperation(IDirectoryInformation dirInfo)
		{
			var directoryStub = new DirectoryStub { ParentDirectory = _currentStub, Name = dirInfo.Name };
			return new ReadDirectoryOperation(directoryStub, dirInfo);
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
				Size = fileInfo.Size,
				ParentDirectory = _currentStub
			};
		}
	}
}
