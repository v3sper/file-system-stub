using System;
using System.Collections.Generic;
using System.Linq;

namespace File.System.Stub
{
	internal static class EnumerableExtensions
	{
		public static bool ElementsEqual<T, TKey>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, TKey> keySelector)
		{
			if (first == null)
			{
				first = Enumerable.Empty<T>();
			}
			if (second == null)
			{
				second = Enumerable.Empty<T>();
			}

			return first.OrderBy(keySelector).SequenceEqual(second.OrderBy(keySelector));
		}
	}
}
