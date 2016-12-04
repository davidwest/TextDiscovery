
using System.Diagnostics;
using System.IO;

using TextDiscovery.Html;

namespace TextDiscovery.DemoConsole.Demos
{
    public class DemoHtmlSearchTextMarker
    {
		//private const string FilePath = @"TextSource\HtmlSample01.html";
		//private const string SearchExpression = "Visitor problem request support leopards";

		private const string FilePath = @"TextSource\HtmlSample02.html";
		private const string SearchExpression = "service university board of regent prize";

		//private const string FilePath = @"TextSource\HtmlSample01.html";
		//private const string SearchExpression = "test text plan fund form show make edit code file data sync math page note mail down load memo path word main cell park link line port type room list";

		public static void Start()
		{
			var sourceHtml = GetHtmlText();

			var marker = Resolver.Get<IHtmlSearchTextMarker>();

			var searchTokenListMaker = Resolver.Get<ISearchTokenListMaker>();

			var searchTokens = searchTokenListMaker.MakeTokensFrom(SearchExpression);

			var result = marker.MarkSearchMatches(sourceHtml, searchTokens);

			Debug.WriteLine(result);
		}

		private static string GetHtmlText()
		{
			return File.ReadAllText(FilePath);
		}
    }
}
