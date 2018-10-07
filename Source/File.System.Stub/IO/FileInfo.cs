using System;
using SystemFileInfo = System.IO.FileInfo;

namespace File.System.Stub.IO
{
	internal class FileInfo : IFileInfo
	{
		private readonly SystemFileInfo _systemFileInfo;

		public FileInfo(SystemFileInfo systemFileInfo)
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
