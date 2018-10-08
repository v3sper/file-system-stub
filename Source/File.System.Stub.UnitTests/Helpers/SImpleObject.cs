using System;

namespace File.System.Stub.UnitTests.Helpers
{
	internal sealed class SimpleObject
	{
		public string Key { get; }
		public int Value { get; }

		public SimpleObject(string key, int value)
		{
			Key = key;
			Value = value;
		}

		public override bool Equals(object obj)
		{
			var other = obj as SimpleObject;
			return other != null &&
				   Key == other.Key &&
				   Value == other.Value;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Key, Value);
		}
	}
}
