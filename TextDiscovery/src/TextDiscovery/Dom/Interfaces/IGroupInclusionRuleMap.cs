
namespace TextDiscovery.Dom
{
    public interface IGroupInclusionRuleMap<in TGroupIndicator> where TGroupIndicator : struct
    {
	    InclusionRule GetInclusionRuleFor(TGroupIndicator groupIndicator);
    }
}
