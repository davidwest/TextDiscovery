
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public class HtmlEllipsisConfig
    {
		private static readonly Dictionary<string, string> EmptyDictionary = new Dictionary<string, string>();

		public string ElementName { get; set; } = "span";
	    public string InnerHtml { get; set; } = "&nbsp;&hellip;&nbsp;";
	    public Dictionary<string, string> Attributes { get; set; } = EmptyDictionary;
    }
}
