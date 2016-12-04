
using System;
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public class HtmlMarkConfig
    {
		private static readonly Dictionary<string, string> EmptyDictionary = new Dictionary<string, string>();

		public string ElementName { get; set; } = "mark";
		public Func<SearchTokenMatchKind, Dictionary<string, string>> GetAttributesFor { get; set; } = matchKind => EmptyDictionary;
    }
}
