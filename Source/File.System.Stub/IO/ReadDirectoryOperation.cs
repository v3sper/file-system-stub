using System.Collections.Generic;
using System.Linq;

namespace File.System.Stub.IO
{
	public class ReadDirectoryOperation : IReadOperation
	{
		private readonly IDirectoryInformation _directoryInfo;

		public DirectoryStub CurrentStub { get; }

		public ReadDirectoryOperation(DirectoryStub directoryStub, IDirectoryInformation directoryInfo)
		{
			CurrentStub = directoryStub;
			_directoryInfo = directoryInfo;
		}

		public IEnumerable<IReadOperation> DoOperation()
		{
			CurrentStub.Files = _directoryInfo.EnumerateFiles().Select(GetFileStubFromFileInfo).ToList();
			var directoryOperationList = _directoryInfo.EnumerateDirectories().Select(CreateReadDirectoryOperation).ToList();
			CurrentStub.Directories = directoryOperationList.Select(dirOperation => dirOperation.CurrentStub).ToList();
			return directoryOperationList;
		}

		private ReadDirectoryOperation CreateReadDirectoryOperation(IDirectoryInformation dirInfo)
		{
			var directoryStub = new DirectoryStub { ParentDirectory = CurrentStub, Name = dirInfo.Name };
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
				ParentDirectory = CurrentStub
			};
		}
	}
}
