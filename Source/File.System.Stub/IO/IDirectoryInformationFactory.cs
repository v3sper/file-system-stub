namespace File.System.Stub.IO
{
	public interface IDirectoryInformationFactory
	{
		IDirectoryInformation Create(string path);
	}
}