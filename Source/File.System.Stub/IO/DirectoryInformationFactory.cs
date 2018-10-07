namespace File.System.Stub.IO
{
	public sealed class DirectoryInformationFactory : IDirectoryInformationFactory
	{
		public IDirectoryInformation Create(string path)
		{
			return new DirectoryInformation(path);
		}
	}
}