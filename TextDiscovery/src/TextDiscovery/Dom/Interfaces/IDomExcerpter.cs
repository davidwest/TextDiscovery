
namespace TextDiscovery.Dom
{
    public interface IDomExcerpter<in TNode>
    {
	    void Trim(TNode node, int maxTokenCount);
    }
}
