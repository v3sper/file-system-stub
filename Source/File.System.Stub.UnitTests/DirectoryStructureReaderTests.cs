using File.System.Stub.IO;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace File.System.Stub.UnitTests
{
	[TestFixture]
	public class DirectoryStructureReaderTests
	{
		private const string RootPath = "root";
		private const string NonExistentPath = "non-existent path";
		private const string MockedFileName = "the file";
		private const bool MockedIsReadOnlyFlag = true;
		private const string NonEmptyDirectoryName = "non empty directory";
		private const string EmptyDirectoryName = "empty directory";
		private const string TwoLevelDirectoryName = "two level directory";
		private static readonly DateTime MockedLastAccessTime = new DateTime(2019, 11, 06, 22, 33, 22);
		private static readonly DateTime MockedLastWriteTime = new DateTime(2018, 10, 07, 22, 33, 22);
		private static readonly DateTime MockedCreationTime = new DateTime(2017, 9, 08, 02, 45, 34);


		private IDirectoryInformationFactory _directoryInformationFactory;
		private IDirectoryInformation _twoLevelDirectory;
		private IDirectoryInformation _nonEmptyDirectory;
		private IDirectoryInformation _emptyDirectory;	

		private IFileInformation _fileInformation;

		private DirectoryStructureReader _reader;

		[OneTimeSetUp]
		public void OneTimeSteup()
		{
			_fileInformation = Substitute.For<IFileInformation>();
			_fileInformation.Name.Returns(MockedFileName);
			_fileInformation.IsReadOnly.Returns(MockedIsReadOnlyFlag);
			_fileInformation.LastAccessTimeUtc.Returns(MockedLastAccessTime);
			_fileInformation.LastWriteTimeUtc.Returns(MockedLastWriteTime);
			_fileInformation.CreationTimeUtc.Returns(MockedCreationTime);

			_nonEmptyDirectory = Substitute.For<IDirectoryInformation>();
			_nonEmptyDirectory.Name.Returns(NonEmptyDirectoryName);
			_nonEmptyDirectory.EnumerateFiles().Returns(new[] { _fileInformation });

			_emptyDirectory = Substitute.For<IDirectoryInformation>();
			_emptyDirectory.Name.Returns(EmptyDirectoryName);
			_emptyDirectory.EnumerateFiles().Returns(Enumerable.Empty<IFileInformation>());
			_emptyDirectory.EnumerateDirectories().Returns(Enumerable.Empty<IDirectoryInformation>());

			_twoLevelDirectory = Substitute.For<IDirectoryInformation>();
			_twoLevelDirectory.Name.Returns(TwoLevelDirectoryName);
			_twoLevelDirectory.EnumerateDirectories().Returns(new[] { _nonEmptyDirectory });

			_directoryInformationFactory = Substitute.For<IDirectoryInformationFactory>();
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

			RootStub result = _reader.Read(RootPath);

			result.Path.Should().Be(RootPath);
			result.Directory.Name.Should().Be(TwoLevelDirectoryName);
			result.Directory.ParentDirectory.Should().BeNull();
			result.Directory.Files.Should().BeEmpty();
			result.Directory.Directories.First().Directories.Should().BeEmpty();
			result.Directory.Directories.First().Files.Count().Should().Be(1);
			result.Directory.Directories.First().Files.First().Name.Should().Be(MockedFileName);
		}

		[Test]
		public void ItShouldReturnStructureForEmptyDirectory()
		{
			_directoryInformationFactory.Create(Arg.Any<string>()).Returns(_emptyDirectory);

			RootStub result = _reader.Read(RootPath);

			result.Path.Should().Be(RootPath);
			result.Directory.Name.Should().Be(EmptyDirectoryName);
			result.Directory.Directories.Should().BeEmpty();
			result.Directory.Files.Should().BeEmpty();
		}

		[Test]
		public void ItShouldReturnFileInfoWithCorrectAttributes()
		{
			_directoryInformationFactory.Create(Arg.Any<string>()).Returns(_nonEmptyDirectory);

			RootStub result = _reader.Read(RootPath);

			result.Path.Should().Be(RootPath);
			result.Directory.Name.Should().Be(NonEmptyDirectoryName);
			result.Directory.Directories.Should().BeEmpty();
			result.Directory.Files.Count().Should().Be(1);

			var fileInfo = result.Directory.Files.First();
			fileInfo.Name.Should().Be(MockedFileName);
			fileInfo.IsReadOnly.Should().Be(MockedIsReadOnlyFlag);
			fileInfo.CreationTime.Should().Be(MockedCreationTime);
			fileInfo.LastAccessTime.Should().Be(MockedLastAccessTime);
			fileInfo.LastWriteTime.Should().Be(MockedLastWriteTime);
		}

		[Test]
		public void ItShouldThrowDirectoryNotFoundExceptionIfRootPathDoesntExist()
		{
			_directoryInformationFactory.Create(Arg.Any<string>()).Throws(new DirectoryNotFoundException());

			Action readingOfNonExistentPath = () => _reader.Read(NonExistentPath);

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
