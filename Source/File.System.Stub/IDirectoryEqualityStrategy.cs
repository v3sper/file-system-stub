namespace File.System.Stub
{
	public interface IDirectoryFileEqualityStrategy
	{
		bool AreEqual(DirectoryStub first, DirectoryStub second);
	}
}