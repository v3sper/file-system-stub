using System.Collections.Generic;

namespace File.System.Stub.IO
{
	public interface IDirectoryInfo
	{
		IEnumerable<IFileInfo> EnumerateFiles();

		IEnumerable<IDirectoryInfo> EnumerateDirectories();

		string Name { get; }

		string Path { get; }
	}
}