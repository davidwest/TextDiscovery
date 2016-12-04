
using System.Collections.Generic;

using TextDiscovery.Dom;


namespace TextDiscovery.Html
{
    public class HtmlSearchTextCompiler<TNode, TGroupIndicator> : IHtmlSearchTextCompiler<TGroupIndicator> where TGroupIndicator : struct
    {
	    private readonly IDomSearchTextCompiler<TNode, TGroupIndicator> _domCompiler;
	    private readonly IHtmlConverter<TNode> _converter;
		
	    public HtmlSearchTextCompiler(IDomSearchTextCompiler<TNode, TGroupIndicator> domCompiler, 
									  IHtmlConverter<TNode> converter)
	    {
		    _domCompiler = domCompiler;
		    _converter = converter;
	    }
		
	    public IEnumerable<SearchTextSliceGroup<TGroupIndicator>> Compile(string html, 
																	      IReadOnlyList<SearchToken> searchTokens, 
																	      int? maxTokenCount = null)
	    {
		    var root = _converter.ConvertToNode(html);

		    return _domCompiler.Compile(root, searchTokens, maxTokenCount);
	    }
    }
}
