
using HtmlAgilityPack;

namespace TextDiscovery.Html.HtmlAgilityPack
{
	public class HtmlConverter : IHtmlConverter<HtmlNode>
	{
		public HtmlNode ConvertToNode(string html) => HtmlNode.CreateNode(html).ParentNode;

		public string Render(HtmlNode node) => node.OuterHtml;
	}
}
