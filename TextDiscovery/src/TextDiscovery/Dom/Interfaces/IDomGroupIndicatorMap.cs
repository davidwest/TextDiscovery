
namespace TextDiscovery.Dom
{
    public interface IDomGroupIndicatorMap<in TNode, out TGroupIndicator>
    {
        TGroupIndicator GetGroupIndicatorFor(TNode node);
    }
}
