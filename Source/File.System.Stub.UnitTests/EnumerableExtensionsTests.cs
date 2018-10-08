using File.System.Stub.UnitTests.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace File.System.Stub.UnitTests
{
	[TestFixture]
	internal static class EnumerableExtensionsTests
	{
		[Test]
		public static void ItShouldReturnTrueWhenEnumerablesAreNull()
		{
			IEnumerable<SimpleObject> first = Enumerable.Empty<SimpleObject>();
			IEnumerable<SimpleObject> second = Enumerable.Empty<SimpleObject>();

			bool result = first.ElementsEqual(second, obj => obj.Key);

			result.Should().BeTrue();
		}

		[Test]
		public static void ItShouldReturnTrueWhenEnumerablesAreEmpty()
		{
			IEnumerable<SimpleObject> first = null;
			IEnumerable<SimpleObject> second = null;

			bool result = first.ElementsEqual(second, obj => obj.Key);

			result.Should().BeTrue();
		}

		[Test]
		public static void ItShouldReturnFalseWhenFirstEnumerableIsNull()
		{
			IEnumerable<SimpleObject> first = null;
			IEnumerable<SimpleObject> second = Enumerable.Empty<SimpleObject>();

			bool result = first.ElementsEqual(second, obj => obj.Key);

			result.Should().BeFalse();
		}

		[Test]
		public static void ItShouldReturnFalseWhenSecondEnumerableIsNull()
		{
			IEnumerable<SimpleObject> first = Enumerable.Empty<SimpleObject>();
			IEnumerable<SimpleObject> second = null;

			bool result = first.ElementsEqual(second, obj => obj.Key);

			result.Should().BeFalse();
		}

		[Test]
		public static void ItShouldReturnTrueWhenEnumerablesContainEqualElements()
		{
			IEnumerable<SimpleObject> first = new List<SimpleObject>() { new SimpleObject("obj1", 123), new SimpleObject("obj2", 234), new SimpleObject("obj3", 345) };
			IEnumerable<SimpleObject> second = new List<SimpleObject>() { new SimpleObject("obj2", 234), new SimpleObject("obj3", 345), new SimpleObject("obj1", 123) };

			bool result = first.ElementsEqual(second, obj => obj.Key);

			result.Should().BeTrue();
		}

		[Test]
		public static void ItShouldReturnFalseWhenEnumerablesContainDifferentNumberOfEqualElements()
		{
			IEnumerable<SimpleObject> first = new List<SimpleObject>() { new SimpleObject("obj1", 123), new SimpleObject("obj2", 234), new SimpleObject("obj2", 234) };
			IEnumerable<SimpleObject> second = new List<SimpleObject>() { new SimpleObject("obj2", 234), new SimpleObject("obj1", 123), new SimpleObject("obj1", 123) };

			bool result = first.ElementsEqual(second, obj => obj.Key);

			result.Should().BeFalse();
		}

		[Test]
		public static void ItShouldReturnFalseWhenEnumerablesCountsDiffer()
		{
			IEnumerable<SimpleObject> first = new List<SimpleObject>() { new SimpleObject("obj1", 123), new SimpleObject("obj2", 234) };
			IEnumerable<SimpleObject> second = new List<SimpleObject>() { new SimpleObject("obj2", 234), new SimpleObject("obj1", 123), new SimpleObject("obj3", 345) };

			bool result = first.ElementsEqual(second, obj => obj.Key);

			result.Should().BeFalse();
		}
	}
}
