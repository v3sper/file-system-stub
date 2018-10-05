using System.Collections.Generic;

namespace File.System.Stub
{
	public sealed class DirectoryStub
	{
		public string Name { get; set; }
		public ICollection<DirectoryStub> Directories { get; set; }
		public ICollection<FileStub> Files { get; set; }
		public DirectoryStub ParentDirectory { get; set; }
	}
}