using System.Collections.Generic;

namespace File.System.Stub
{
	public sealed class RootStub
	{
		public string Path { get; set; }
		public DirectoryStub Directory { get; set; }
	}
}