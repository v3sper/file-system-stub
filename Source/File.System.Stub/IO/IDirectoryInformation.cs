using System.Collections.Generic;

namespace File.System.Stub.IO
{
	public interface IDirectoryInformation
	{
		IEnumerable<IFileInformation> EnumerateFiles();

		IEnumerable<IDirectoryInformation> EnumerateDirectories();

		string Name { get; }

		string Path { get; }
	}
}