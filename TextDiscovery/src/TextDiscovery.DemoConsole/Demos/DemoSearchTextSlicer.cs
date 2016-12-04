
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Humanizer;

namespace TextDiscovery.DemoConsole.Demos
{
    public static class DemoSearchTextSlicer
    {
        private const string SearchExpression = 
@"
Are you travelling to Europe this week? 
Precise, immediate, absolute, land, injury, watching, minute, photograph, occasion, incidentally, glancing, intelligent, laughed";

        public static void Start()
        {
            var source = GetSourceText();
            var slicer = Resolver.Get<ISearchTextSlicer>();

            var searchTokenListMaker = Resolver.Get<ISearchTokenListMaker>();

            var searchTokens = searchTokenListMaker.MakeTokensFrom(SearchExpression);

            Display(searchTokens);

            var slices = slicer.CreateSlices(source, searchTokens, 100000);

            Display(slices);
        }

        private static void Display(IEnumerable<SearchToken> tokens)
        {
            foreach (var token in tokens)
            {
                Debug.WriteLine(token);
            }
        }

        private static void Display(IEnumerable<SearchTextSlice> slices)
        {
            var builder = new StringBuilder();

            var i = 0;
            foreach (var slice in slices.Where(s => s.IsToken))
            {
                if (i % 30 == 0)
                {
                    builder.AppendLine();
                }

                if (slice.Match == null || slice.Match.Kind == SearchTokenMatchKind.ContainsOriginal)
                {
                    builder.Append($"{slice.Text} ");
                    i++;
                }
                else
                {
                    builder.AppendLine($"\n\n***** {slice.Text} : {slice.Match.Kind.Humanize()} *****");
                    i = 0;
                }

	            if (slice.IsOverflow)
	            {
		            builder.Append("<<< OVERFLOW >>>");
	            }
            }

            Debug.WriteLine(builder);
        }


        private static string GetSourceText()
        {
            return File.ReadAllText(@"TextSource\big.txt");
        }
    }
}
