using File.System.Stub.IO;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace File.System.Stub.UnitTests
{
	[TestFixture]
	internal static class DirectoryStructureReaderTests
	{
		//private const string folder = @"C:\GIT\file-system-stub";
		private const string folder = @"Z:\";

		public static async Task ItShouldReturnProperStructure()
		{
			DirectoryStructureReader reader = new DirectoryStructureReader(new DirectoryInformationFactory());
			var stub = await reader.ReadAsync(folder, CancellationToken.None).ConfigureAwait(false);
		}
	}
}
