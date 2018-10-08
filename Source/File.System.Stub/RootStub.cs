using System.Collections.Generic;

namespace File.System.Stub
{
	public sealed class RootStub
	{
		public string Path { get; set; }
		public DirectoryStub Directory { get; set; }

		public override bool Equals(object obj)
		{
			var stub = obj as RootStub;
			return stub != null && Path == stub.Path;
		}

		public override int GetHashCode()
		{
			var hashCode = -1342469650;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Path);
			hashCode = hashCode * -1521134295 + EqualityComparer<DirectoryStub>.Default.GetHashCode(Directory);
			return hashCode;
		}
	}
}