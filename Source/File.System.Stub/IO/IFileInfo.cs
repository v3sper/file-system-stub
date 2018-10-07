using System;

namespace File.System.Stub.IO
{
	public interface IFileInfo
	{
		DateTime CreationTimeUtc { get; }
		bool IsReadOnly { get; }
		DateTime LastAccessTimeUtc { get; }
		DateTime LastWriteTimeUtc { get; }
		string Name { get; }
	}
}