
using TextDiscovery.Dom;
using TextDiscovery.Html;

namespace TextDiscovery.DemoConsole
{
    public class HtmlSearchTextCompiler<TNode> : HtmlSearchTextCompiler<TNode, GroupIndicator>, IHtmlSearchTextCompiler 
    {
	    public HtmlSearchTextCompiler(IDomSearchTextCompiler<TNode, GroupIndicator> domCompiler, 
									  IHtmlConverter<TNode> converter) 
			: base(domCompiler, converter)
	    { }
    }
}
