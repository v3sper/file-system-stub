using System;

namespace File.System.Stub.IO
{
	public interface IFileInformation
	{
		DateTime CreationTimeUtc { get; }
		bool IsReadOnly { get; }
		DateTime LastAccessTimeUtc { get; }
		DateTime LastWriteTimeUtc { get; }
		string Name { get; }
		long Size { get; }
	}
}