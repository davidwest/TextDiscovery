
using System;
using System.Collections.Generic;
using System.Linq;

namespace TextDiscovery
{
    public static class TokenizationExtensions
    {	
		private static readonly ITextSlicer Slicer = TextSlicer.Default;

		public static IEnumerable<string> GetTokens(this string source)
        {
			return Slicer.CreateSlices(source)
				   .Where(seg => seg.IsToken)
				   .Select(seg => seg.Text);
		}
        
        public static Dictionary<string, int> GetTokenCounts(this string source, Func<string, bool> isValidToken = null)
        {
			isValidToken = isValidToken ?? (word => true);

			var wordCounts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

			source.GetTokens()
			.Where(isValidToken)
			.ForEach(word =>
			{
				if (wordCounts.ContainsKey(word)) wordCounts[word] += 1;
				else wordCounts.Add(word, 1);
			});

			return wordCounts;
		}
    }
}
