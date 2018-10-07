using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace File.System.Stub.IO
{
	internal sealed class DirectoryInformation : IDirectoryInformation
	{
		private readonly DirectoryInfo _systemDirectoryInfo;

		public DirectoryInformation(string path)
		{
			_systemDirectoryInfo = new DirectoryInfo(path);
		}

		public DirectoryInformation(DirectoryInfo systemDirectoryInfo)
		{
			_systemDirectoryInfo = systemDirectoryInfo;
		}

		public IEnumerable<IFileInformation> EnumerateFiles()
		{
			return _systemDirectoryInfo.EnumerateFiles().Select(fi => new FileInformation(fi));
		}

		public IEnumerable<IDirectoryInformation> EnumerateDirectories()
		{
			return _systemDirectoryInfo.EnumerateDirectories().Select(di => new DirectoryInformation(di));
		}

		public string Name => _systemDirectoryInfo.Name;

		public string Path => _systemDirectoryInfo.FullName;
	}
}
