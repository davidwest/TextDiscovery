
namespace TextDiscovery.Dom
{
    public class DomMarkerNodeFactory<TNode> : IDomMarkerNodeFactory<TNode>
    {
	    private readonly IDomElementNodeFactory<TNode> _innferFactory;
	    private readonly HtmlMarkConfig _config;

	    public DomMarkerNodeFactory(IDomElementNodeFactory<TNode> innerFactory, HtmlMarkConfig config)
	    {
		    _innferFactory = innerFactory;
		    _config = config;
	    }

	    public TNode CreateMarkerNode(TNode root, SearchTokenMatchKind matchKind)
	    {
		    var attributes = _config.GetAttributesFor(matchKind);
		    var node = _innferFactory.CreateElementNode(root, _config.ElementName, attributes);

		    return node;
	    }
    }
}
