namespace File.System.Stub.IO
{
	internal sealed class DirectoryInformationFactory : IDirectoryInformationFactory
	{
		public IDirectoryInformation Create(string path)
		{
			return new DirectoryInformation(path);
		}
	}
}