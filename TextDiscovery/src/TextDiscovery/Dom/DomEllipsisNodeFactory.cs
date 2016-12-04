
namespace TextDiscovery.Dom
{
    public class DomEllipsisNodeFactory<TNode> : IDomEllipsisNodeFactory<TNode>
    {
	    private readonly IDomElementNodeFactory<TNode> _innerFactory;
	    private readonly HtmlEllipsisConfig _config;

	    public DomEllipsisNodeFactory(IDomElementNodeFactory<TNode> innerFactory, HtmlEllipsisConfig config)
	    {
		    _innerFactory = innerFactory;
		    _config = config;
	    }

	    public TNode CreateEllipsisNode(TNode root)
	    {
		    return _innerFactory.CreateElementNode(root, _config.ElementName, _config.Attributes, _config.InnerHtml);
	    }
    }
}
