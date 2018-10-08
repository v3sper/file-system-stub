using System.Collections.Generic;
using File.System.Stub.Helpers;

namespace File.System.Stub
{
	public sealed class DirectoryStub
	{
		public string Name { get; set; }
		public ICollection<DirectoryStub> Directories { get; set; }
		public ICollection<FileStub> Files { get; set; }
		public DirectoryStub ParentDirectory { get; set; }

		public override bool Equals(object obj)
		{
			var stub = obj as DirectoryStub;
			return stub != null &&
				   Name == stub.Name &&
				   Directories.ElementsEqual(stub.Directories, dir => dir.Name) &&
				   Files.ElementsEqual(stub.Files, file => file.Name);
		}

		public override int GetHashCode()
		{
			var hashCode = 1768900658;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = hashCode * -1521134295 + (ParentDirectory == null ? 0 : EqualityComparer<DirectoryStub>.Default.GetHashCode(ParentDirectory));
			return hashCode;
		}
	}
}