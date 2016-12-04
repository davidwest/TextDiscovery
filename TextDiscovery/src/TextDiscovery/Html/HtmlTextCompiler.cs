
using System.Collections.Generic;

using TextDiscovery.Dom;

namespace TextDiscovery.Html
{
    public class HtmlTextCompiler<TNode, TGroupIndicator> : IHtmlTextCompiler<TGroupIndicator> where TGroupIndicator : struct
    {
	    private readonly IDomTextCompiler<TNode, TGroupIndicator> _domCompiler;
	    private readonly IHtmlConverter<TNode> _converter;

	    public HtmlTextCompiler(IDomTextCompiler<TNode, TGroupIndicator> domCompiler, 
								IHtmlConverter<TNode> converter)
	    {
		    _domCompiler = domCompiler;
		    _converter = converter;
	    }
		
	    public IEnumerable<TextSliceGroup<TGroupIndicator>> Compile(string rootSource, int? maxTokenCount = null)
	    {
		    var root = _converter.ConvertToNode(rootSource);

		    return _domCompiler.Compile(root, maxTokenCount);
	    }
    }
}
