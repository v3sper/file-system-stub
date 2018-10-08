namespace File.System.Stub.IO
{
	internal interface IDirectoryInformationFactory
	{
		IDirectoryInformation Create(string path);
	}
}