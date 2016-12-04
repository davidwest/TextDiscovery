
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

using TextDiscovery.Html;


namespace TextDiscovery.AngleSharp.Html
{
    public class HtmlConverter : IHtmlConverter<INode>
    {
		public INode ConvertToNode(string html)
		{
			var parser = new HtmlParser();
			return parser.Parse(html)?.Body;
		}

		public string Render(INode node)
		{
			var body = node as IHtmlBodyElement;

			return body != null ? body.InnerHtml : ((IHtmlElement)node).OuterHtml;
		}
	}
}
