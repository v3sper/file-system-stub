namespace File.System.Stub
{
	internal sealed class DirectoryPair
	{
		public DirectoryStub First { get; }

		public DirectoryStub Second { get; }

		public DirectoryPair(DirectoryStub first, DirectoryStub second)
		{
			First = first;
			Second = second;
		}
	}
}
