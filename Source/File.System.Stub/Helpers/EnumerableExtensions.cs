using System;
using System.Collections.Generic;
using System.Linq;

namespace File.System.Stub.Helpers
{
	internal static class EnumerableExtensions
	{
		public static bool ElementsEqual<T, TKey>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TKey> keySelector)
		{
			if(first == null)
			{
				if(second == null)
				{
					return true;
				}
				return false;
			}
			return first.OrderBy(keySelector).SequenceEqual(second.OrderBy(keySelector));
		}
	}
}
