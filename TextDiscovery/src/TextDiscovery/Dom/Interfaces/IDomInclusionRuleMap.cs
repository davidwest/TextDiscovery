
namespace TextDiscovery.Dom
{
    public interface IDomInclusionRuleMap<in TNode>
    {
	    InclusionRule? GetInclusionRuleFor(TNode node);
    }
}
