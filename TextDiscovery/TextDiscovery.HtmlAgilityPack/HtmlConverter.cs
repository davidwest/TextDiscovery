
using HtmlAgilityPack;
using TextDiscovery.Html;

namespace TextDiscovery.HtmlAgilityPack
{
	public class HtmlConverter : IHtmlConverter<HtmlNode>
	{
		public HtmlNode ConvertToNode(string html) => HtmlNode.CreateNode(html);

		public string Render(HtmlNode node) => node.OuterHtml;
	}
}
