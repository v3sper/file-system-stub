using System;
using System.Collections.Generic;

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

		public override bool Equals(object obj)
		{
			var stub = obj as FileStub;
			return stub != null &&
				   Name == stub.Name &&
				   Size == stub.Size &&
				   CreationTime == stub.CreationTime &&
				   LastWriteTime == stub.LastWriteTime &&
				   LastAccessTime == stub.LastAccessTime &&
				   IsReadOnly == stub.IsReadOnly;
		}

		public override int GetHashCode()
		{
			var hashCode = -1716570395;
			hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
			hashCode = hashCode * -1521134295 + Size.GetHashCode();
			hashCode = hashCode * -1521134295 + CreationTime.GetHashCode();
			hashCode = hashCode * -1521134295 + LastWriteTime.GetHashCode();
			hashCode = hashCode * -1521134295 + LastAccessTime.GetHashCode();
			hashCode = hashCode * -1521134295 + IsReadOnly.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<DirectoryStub>.Default.GetHashCode(ParentDirectory);
			return hashCode;
		}
	}
}