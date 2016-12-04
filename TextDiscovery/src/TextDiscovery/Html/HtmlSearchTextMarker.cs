
using System.Collections.Generic;

using TextDiscovery.Dom;

namespace TextDiscovery.Html
{
    public class HtmlSearchTextMarker<TNode> : IHtmlSearchTextMarker
    {
	    private readonly IDomSearchTextMarker<TNode> _domMarker;
	    private readonly IHtmlConverter<TNode> _converter;

	    public HtmlSearchTextMarker(IDomSearchTextMarker<TNode> domMarker, 
									IHtmlConverter<TNode> converter)
	    {
		    _domMarker = domMarker;
		    _converter = converter;
	    }

	    public string MarkSearchMatches(string sourceHtml, IReadOnlyList<SearchToken> searchTokens)
	    {
		    var root = _converter.ConvertToNode(sourceHtml);

		    _domMarker.MarkSearchMatches(root, searchTokens);

		    var rendered = _converter.Render(root);

			return rendered;
	    }
    }
}
