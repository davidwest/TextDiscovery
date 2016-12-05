
using System.Diagnostics;
using System.IO;

using TextDiscovery.Html;

namespace TextDiscovery.DemoConsole.Demos
{
    public static class DemoHtmlExcerpter
    {
		private const string FilePath = @"..\..\TextSource\HtmlSample04.html";

		public static void Start()
	    {
			var sourceHtml = GetHtmlText();

		    var excerpter = Resolver.Get<IHtmlExcerpter>();

			var result = excerpter.Trim(sourceHtml, 112);
			Debug.WriteLine(result);

			result = excerpter.Trim(sourceHtml, 120);
			Debug.WriteLine(result);

			result = excerpter.Trim(sourceHtml, 118);
			Debug.WriteLine(result);

			result = excerpter.Trim(sourceHtml, 161);
			Debug.WriteLine(result);

			result = excerpter.Trim(sourceHtml, 162);
			Debug.WriteLine(result);

			result = excerpter.Trim(sourceHtml, 189);
			Debug.WriteLine(result);

			result = excerpter.Trim(sourceHtml, 190);
			Debug.WriteLine(result);
		}

		private static string GetHtmlText()
		{
			return File.ReadAllText(FilePath);
		}
	}
}
