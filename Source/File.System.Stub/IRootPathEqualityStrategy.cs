namespace File.System.Stub
{
	public interface IRootPathEqualityStrategy
	{
		bool AreEqual(string first, string second);
	}
}