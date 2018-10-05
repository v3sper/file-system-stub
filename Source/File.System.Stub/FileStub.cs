using System;

namespace File.System.Stub
{
	public sealed class FileStub
	{
		public string Name { get; set; }
		public long Size { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime LastWriteTime { get; set; }
		public DateTime LastAccessTime { get; set; }
		public bool IsReadOnly { get; set; }
		public DirectoryStub ParentDirectory { get; set; }
	}
}