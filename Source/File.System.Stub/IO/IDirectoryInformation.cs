using System.Collections.Generic;

namespace File.System.Stub.IO
{
	internal interface IDirectoryInformation
	{
		IEnumerable<IFileInformation> EnumerateFiles();

		IEnumerable<IDirectoryInformation> EnumerateDirectories();

		string Name { get; }

		string Path { get; }
	}
}