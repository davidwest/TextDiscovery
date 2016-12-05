
using System.Collections.Generic;
using HtmlAgilityPack;

namespace TextDiscovery.Dom.HtmlAgilityPack
{
	public class DomNodeFactory : IDomNodeFactory<HtmlNode>
	{
		private static readonly Dictionary<string, string> EmptyAttributes = new Dictionary<string, string>();

		public HtmlNode CreateTextNode(HtmlNode root, string text)
		{
			return HtmlNode.CreateNode(text);
		}

		public HtmlNode CreateElementNode(HtmlNode root, string elementName, Dictionary<string, string> attributes, string innerHtml = null)
		{
			var node =  HtmlNode.CreateNode($"<{elementName}>{innerHtml}</{elementName}>");

			foreach (var attr in attributes)
			{
				node.SetAttributeValue(attr.Key, attr.Value);
			}

			return node;
		}

		public HtmlNode CreateElementNode(HtmlNode root, string elementName, string innerHtml = null)
		{
			return CreateElementNode(root, elementName, EmptyAttributes, innerHtml);
		}
	}
}
