using System;
using System.IO;

namespace File.System.Stub.IO
{
	internal class FileInformation : IFileInformation
	{
		private readonly FileInfo _systemFileInfo;

		public FileInformation(FileInfo systemFileInfo)
		{
			_systemFileInfo = systemFileInfo;
		}

		public string Name => _systemFileInfo.Name;

		public string Path => _systemFileInfo.FullName;

		public DateTime CreationTimeUtc => _systemFileInfo.CreationTimeUtc;

		public bool IsReadOnly => _systemFileInfo.IsReadOnly;

		public DateTime LastAccessTimeUtc => _systemFileInfo.LastAccessTimeUtc;

		public DateTime LastWriteTimeUtc => _systemFileInfo.LastWriteTimeUtc;
	}
}
