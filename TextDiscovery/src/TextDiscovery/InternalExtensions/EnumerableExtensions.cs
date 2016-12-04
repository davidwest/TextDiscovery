
using System;
using System.Collections.Generic;
using System.Linq;

namespace TextDiscovery
{
    internal static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> performAction)
        {
            foreach (var item in sequence)
            {
                performAction(item);
            }
        }

		public static string SerializeToString<T>(this IEnumerable<T> values)
		{
			return values.SerializeToString(string.Empty, string.Empty, string.Empty);
		}

		public static string SerializeToString<T>(this IEnumerable<T> values,
												  char separator,
												  string wrapperStart = "",
												  string wrapperEnd = "")
		{
			return values.SerializeToString(separator.ToString(), wrapperStart, wrapperEnd);
		}

		public static string SerializeToString<T>(this IEnumerable<T> values,
												  string separator,
												  string wrapperStart = "",
												  string wrapperEnd = "")
		{
			var list = values.ToList();

			if (list.Count == 0)
			{
				return string.Empty;
			}

			var result = list.Select(val => $"{wrapperStart}{val}{wrapperEnd}")
							 .Aggregate((a, b) => $"{a}{separator}{b}");

			return result;
		}
	}
}
