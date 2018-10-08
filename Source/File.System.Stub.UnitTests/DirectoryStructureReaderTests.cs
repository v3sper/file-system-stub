using File.System.Stub.IO;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace File.System.Stub.UnitTests
{
	[TestFixture]
	public class DirectoryStructureReaderTests
	{
		private string _rootPath;
		private string _nonExistentPath;
		private string _mockedFileName;
		private bool _mockedIsReadOnlyFlag;
		private string _nonEmptyDirectoryName;
		private string _emptyDirectoryName;
		private string _twoLevelDirectoryName;
		private DateTime _mockedLastAccessTime;
		private DateTime _mockedLastWriteTime;
		private DateTime _mockedCreationTime;

		private IDirectoryInformationFactory _directoryInformationFactory;
		private IDirectoryInformation _twoLevelDirectory;
		private IDirectoryInformation _nonEmptyDirectory;
		private IDirectoryInformation _emptyDirectory;	

		private IFileInformation _fileInformation;

		private DirectoryStructureReader _reader;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			_rootPath = "root";
			_nonExistentPath = "non-existent path";
			_mockedFileName = "the file";
			_mockedIsReadOnlyFlag = true;
			_nonEmptyDirectoryName = "non empty directory";
			_emptyDirectoryName = "empty directory";
			_twoLevelDirectoryName = "two level directory";

			_mockedLastAccessTime = new DateTime(2019, 11, 06, 22, 33, 22);
			_mockedLastWriteTime = new DateTime(2018, 10, 07, 22, 33, 22);
			_mockedCreationTime = new DateTime(2017, 9, 08, 02, 45, 34);

			_fileInformation = Substitute.For<IFileInformation>();
			_fileInformation.Name.Returns(_mockedFileName);
			_fileInformation.IsReadOnly.Returns(_mockedIsReadOnlyFlag);
			_fileInformation.LastAccessTimeUtc.Returns(_mockedLastAccessTime);
			_fileInformation.LastWriteTimeUtc.Returns(_mockedLastWriteTime);
			_fileInformation.CreationTimeUtc.Returns(_mockedCreationTime);

			_nonEmptyDirectory = Substitute.For<IDirectoryInformation>();
			_nonEmptyDirectory.Name.Returns(_nonEmptyDirectoryName);
			_nonEmptyDirectory.EnumerateFiles().Returns(new[] { _fileInformation });

			_emptyDirectory = Substitute.For<IDirectoryInformation>();
			_emptyDirectory.Name.Returns(_emptyDirectoryName);
			_emptyDirectory.EnumerateFiles().Returns(Enumerable.Empty<IFileInformation>());
			_emptyDirectory.EnumerateDirectories().Returns(Enumerable.Empty<IDirectoryInformation>());

			_twoLevelDirectory = Substitute.For<IDirectoryInformation>();
			_twoLevelDirectory.Name.Returns(_twoLevelDirectoryName);
			_twoLevelDirectory.EnumerateDirectories().Returns(new[] { _nonEmptyDirectory });
		}

		[SetUp]
		public void SetUp()
		{
			_directoryInformationFactory = Substitute.For<IDirectoryInformationFactory>();
			_reader = new DirectoryStructureReader(_directoryInformationFactory);
		}

		[Test]
		public void ItShouldReturnStructureForTwoLevelDirectory()
		{
			_directoryInformationFactory.Create(Arg.Any<string>()).Returns(_twoLevelDirectory);

			RootStub result = _reader.Read(_rootPath);

			result.Path.Should().Be(_rootPath);
			result.Directory.Name.Should().Be(_twoLevelDirectoryName);
			result.Directory.ParentDirectory.Should().BeNull();
			result.Directory.Files.Should().BeEmpty();
			result.Directory.Directories.First().Directories.Should().BeEmpty();
			result.Directory.Directories.First().Files.Count().Should().Be(1);
			result.Directory.Directories.First().Files.First().Name.Should().Be(_mockedFileName);
		}

		[Test]
		public void ItShouldReturnStructureForEmptyDirectory()
		{
			_directoryInformationFactory.Create(_rootPath).Returns(_emptyDirectory);

			RootStub result = _reader.Read(_rootPath);

			result.Path.Should().Be(_rootPath);
			result.Directory.Name.Should().Be(_emptyDirectoryName);
			result.Directory.Directories.Should().BeEmpty();
			result.Directory.Files.Should().BeEmpty();
		}

		[Test]
		public void ItShouldReturnFileInfoWithCorrectAttributes()
		{
			_directoryInformationFactory.Create(_rootPath).Returns(_nonEmptyDirectory);

			RootStub result = _reader.Read(_rootPath);

			result.Path.Should().Be(_rootPath);
			result.Directory.Name.Should().Be(_nonEmptyDirectoryName);
			result.Directory.Directories.Should().BeEmpty();
			result.Directory.Files.Count().Should().Be(1);

			var fileInfo = result.Directory.Files.First();
			fileInfo.Name.Should().Be(_mockedFileName);
			fileInfo.IsReadOnly.Should().Be(_mockedIsReadOnlyFlag);
			fileInfo.CreationTime.Should().Be(_mockedCreationTime);
			fileInfo.LastAccessTime.Should().Be(_mockedLastAccessTime);
			fileInfo.LastWriteTime.Should().Be(_mockedLastWriteTime);
		}

		[Test]
		public void ItShouldThrowDirectoryNotFoundExceptionIfRootPathDoesNotExist()
		{
			_directoryInformationFactory.Create(_rootPath).Throws(new DirectoryNotFoundException());

			Action readingOfNonExistentPath = () => _reader.Read(_nonExistentPath);

			readingOfNonExistentPath.Should().Throw<DirectoryNotFoundException>();
		}

		[Test]
		public void ItShouldThrowArgumentNullExceptionWhenRootPathIsNull()
		{
			Action readingOfNonExistentPath = () => _reader.Read(null);

			readingOfNonExistentPath.Should().Throw<ArgumentNullException>();
		}
	}
}
