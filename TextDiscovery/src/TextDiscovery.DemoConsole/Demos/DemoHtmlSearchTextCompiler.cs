

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TextDiscovery.DemoConsole.Demos
{
    public static class DemoHtmlSearchTextCompiler
    {
		private const string FilePath = @"TextSource\HtmlSample01.html";
		private const string SearchExpression = "Visitor problem";

		//private const string FilePath = @"TextSource\HtmlSample02.html";
		//private const string SearchExpression = "teach learn";

		private static IHtmlSearchTextCompiler _compiler;
	    private static IReadOnlyList<SearchToken> _searchTokens;
	    private static string _html;

		public static void Start()
		{
			_html = GetHtmlText();
			_compiler = Resolver.Get<IHtmlSearchTextCompiler>();

		    var searchTokenListMaker = Resolver.Get<ISearchTokenListMaker>();

		    _searchTokens = searchTokenListMaker.MakeTokensFrom(SearchExpression);

			Demo(100);
		}

	    public static void Demo(int? maxTokenCount = null)
	    {
			var groups = _compiler.Compile(_html, _searchTokens, maxTokenCount);

			var builder = new StringBuilder();

			foreach (var grp in groups)
			{
				builder.AppendLine($"\n\n--- {grp.GroupIndicator} ---");

				foreach (var slice in grp.Slices)
				{
					var isMatch = slice.Match != null && slice.Match.Kind != SearchTokenMatchKind.ContainsOriginal;
					var matchStr = isMatch ? "***" : "";

					builder.Append(slice.IsToken ? $"[{matchStr}{slice.Text}{(slice.IsOverflow ? " !!! OVERFLOW !!!" : "")}{matchStr}]" : slice.Text);
				}
			}

			Debug.WriteLine(builder.ToString());
		}

	    private static string GetHtmlText()
	    {
			return File.ReadAllText(FilePath);
		}
	}
}
