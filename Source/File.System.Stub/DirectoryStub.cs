using System.Collections.Generic;
using Newtonsoft.Json;

namespace File.System.Stub
{
	public sealed class DirectoryStub
	{
		public string Name { get; set; }
		public ICollection<DirectoryStub> Directories { get; set; }
		public ICollection<FileStub> Files { get; set; }
		public DirectoryStub ParentDirectory { get; set; }

		[JsonIgnore]
		public string Path => $"{ParentDirectory?.Path}/{Name}";
	}
}