using FluentAssertions;
using NUnit.Framework;
using System;
using System.IO;
using System.IO.Compression;
using FileSystemFile = System.IO.File;

namespace File.System.Stub.IntegrationTests
{
	[TestFixture]
	public class DirectoryStructureReaderTests
	{
		private string _basicSetTempFolderPath;
		private RootStub _basicSetStub;

		[OneTimeSetUp]
		public void SetUpFixture()
		{
			string tempFolderPath = Path.Combine(Path.GetTempPath(), "File.System.Stub.IntegrationTests");
			if (!Directory.Exists(tempFolderPath))
			{
				Directory.CreateDirectory(tempFolderPath);
			}
			_basicSetTempFolderPath = Path.Combine(tempFolderPath, "SmallSet");
			if(Directory.Exists(_basicSetTempFolderPath))
			{
				Directory.Delete(_basicSetTempFolderPath, true);
			}
			if (!Directory.Exists(_basicSetTempFolderPath))
			{
				Directory.CreateDirectory(_basicSetTempFolderPath);
			}
			ZipFile.ExtractToDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "SmallSet.zip"), _basicSetTempFolderPath);
			_basicSetStub = Serializer.Deserialize<RootStub>(FileSystemFile.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "SmallSet.json")));
		}

		[OneTimeTearDown]
		public void OneTimeTeardown()
		{
			if (Directory.Exists(_basicSetTempFolderPath))
			{
				Directory.Delete(_basicSetTempFolderPath, true);
			}
		}

		[Test]
		public void ItShouldReadSimpleFolderStructure()
		{
			DirectoryStructureReader reader = new DirectoryStructureReader();

			RootStub result = reader.Read(_basicSetTempFolderPath);

			result.Should().Be(_basicSetStub);
		}
	}
}
