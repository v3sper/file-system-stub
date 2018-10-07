namespace File.System.Stub.IO
{
	public sealed class DirectoryInfoFactory : IDirectoryInfoFactory
	{
		public IDirectoryInfo Create(string path)
		{
			return new DirectoryInfo(path);
		}
	}
}