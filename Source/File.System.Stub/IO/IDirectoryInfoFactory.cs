namespace File.System.Stub.IO
{
	public interface IDirectoryInfoFactory
	{
		IDirectoryInfo Create(string path);
	}
}