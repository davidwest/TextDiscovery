
using TextDiscovery.Dom;

namespace TextDiscovery.Html
{
    public class HtmlExcerpter<TNode> : IHtmlExcerpter
    {
		private readonly IDomExcerpter<TNode> _domExcerpter;
	    private readonly IHtmlConverter<TNode> _converter;

		public HtmlExcerpter(IDomExcerpter<TNode> domExcerpter, 
							 IHtmlConverter<TNode> converter)
		{
			_domExcerpter = domExcerpter;
			_converter = converter;
		}

	    public string Trim(string html, int maxTokenCount)
	    {
		    var root = _converter.ConvertToNode(html);

			_domExcerpter.Trim(root, maxTokenCount);

		    var rendered = _converter.Render(root);

		    return rendered;
	    }
    }
}
