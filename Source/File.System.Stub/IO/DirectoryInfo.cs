using System.Collections.Generic;
using System.Linq;
using SystemDirectoryInfo = System.IO.DirectoryInfo;

namespace File.System.Stub.IO
{
	internal sealed class DirectoryInfo : IDirectoryInfo
	{
		private readonly SystemDirectoryInfo _systemDirectoryInfo;

		public DirectoryInfo(string path)
		{
			_systemDirectoryInfo = new SystemDirectoryInfo(path);
		}

		public DirectoryInfo(SystemDirectoryInfo systemDirectoryInfo)
		{
			_systemDirectoryInfo = systemDirectoryInfo;
		}

		public IEnumerable<IFileInfo> EnumerateFiles()
		{
			return _systemDirectoryInfo.EnumerateFiles().Select(fi => new FileInfo(fi));
		}

		public IEnumerable<IDirectoryInfo> EnumerateDirectories()
		{
			return _systemDirectoryInfo.EnumerateDirectories().Select(di => new DirectoryInfo(di));
		}

		public string Name => _systemDirectoryInfo.Name;

		public string Path => _systemDirectoryInfo.FullName;
	}
}
