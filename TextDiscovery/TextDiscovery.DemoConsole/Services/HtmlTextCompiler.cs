
using TextDiscovery.Dom;
using TextDiscovery.Html;

namespace TextDiscovery.DemoConsole
{
    public class HtmlTextCompiler<TNode> : HtmlTextCompiler<TNode, GroupIndicator>, IHtmlTextCompiler 
    {
	    public HtmlTextCompiler(IDomTextCompiler<TNode, GroupIndicator> domCompiler, 
								IHtmlConverter<TNode> converter) 
			: base(domCompiler, converter)
	    { }
    }
}
