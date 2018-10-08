using System;
using System.IO;

namespace File.System.Stub.IO
{
	internal sealed class FileInformation : IFileInformation
	{
		private readonly FileInfo _systemFileInfo;

		public FileInformation(FileInfo systemFileInfo)
		{
			_systemFileInfo = systemFileInfo;
		}

		public string Name => _systemFileInfo.Name;

		public string Path => _systemFileInfo.FullName;

		public DateTime CreationTimeUtc => _systemFileInfo.CreationTime;

		public bool IsReadOnly => _systemFileInfo.IsReadOnly;

		public DateTime LastAccessTimeUtc => _systemFileInfo.LastAccessTime;

		public DateTime LastWriteTimeUtc => _systemFileInfo.LastWriteTime;

		public long Size => _systemFileInfo.Length;
	}
}
