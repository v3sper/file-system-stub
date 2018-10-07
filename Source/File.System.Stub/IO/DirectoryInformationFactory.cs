namespace File.System.Stub.IO
{
	public sealed class DirectoryInfoFactory : IDirectoryInfoFactory
	{
		public IDirectoryInformation Create(string path)
		{
			return new DirectoryInformation(path);
		}
	}
}