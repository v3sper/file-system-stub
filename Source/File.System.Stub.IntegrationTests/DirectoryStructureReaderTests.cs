using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace File.System.Stub.IntegrationTests
{
	[TestFixture]
	public class DirectoryStructureReaderTests
	{
		private string _basicSetTempFolderPath;

		[OneTimeSetUp]
		public void SetUpFixture()
		{
			string tempFolderPath = Path.Combine(Path.GetTempPath(), "File.System.Stub.IntegrationTests");
			if (!Directory.Exists(tempFolderPath))
			{
				Directory.CreateDirectory(tempFolderPath);
			}
			_basicSetTempFolderPath = Path.Combine(tempFolderPath, "BasicSet");
			if(Directory.Exists(_basicSetTempFolderPath))
			{
				Directory.Delete(_basicSetTempFolderPath, true);
			}
			if (!Directory.Exists(_basicSetTempFolderPath))
			{
				Directory.CreateDirectory(_basicSetTempFolderPath);
			}
			ZipFile.ExtractToDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "BasicSet.zip"), _basicSetTempFolderPath);
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

			Action readFolder = () => reader.Read(_basicSetTempFolderPath);

			readFolder.Should().NotThrow();
		}
	}
}
