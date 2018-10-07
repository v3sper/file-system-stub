using File.System.Stub.UnitTests.Helpers;
using FluentAssertions;
using NUnit.Framework;

namespace File.System.Stub.UnitTests
{
	[TestFixture]
	internal static class SerializerTests
	{
		[Test]
		public static void ItShouldHandleParentChildRelation()
		{
			Parent givenParent = new Parent
			{
				FirstChild = new Child(),
				SecondChild = new Child()
			};
			givenParent.FirstChild.Parent = givenParent;
			givenParent.SecondChild.Parent = givenParent;

			// ACT
			string json = Serializer.Serialize(givenParent);
			Parent actualParent = Serializer.Deserialize<Parent>(json);

			// ASSERT
			actualParent.Should().Be(actualParent.FirstChild.Parent);
			actualParent.Should().Be(actualParent.SecondChild.Parent);
			actualParent.FirstChild.Should().NotBe(actualParent.SecondChild);
		}
	}
}