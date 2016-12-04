
namespace TextDiscovery.Dom
{
    public interface IDomMarkerNodeFactory<TNode>
    {
	    TNode CreateMarkerNode(TNode root, SearchTokenMatchKind matchKind);
    }
}
