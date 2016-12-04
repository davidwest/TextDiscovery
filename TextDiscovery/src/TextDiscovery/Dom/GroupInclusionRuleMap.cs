

using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public class GroupInclusionRuleMap<TGroupIndicator> : IGroupInclusionRuleMap<TGroupIndicator> where TGroupIndicator : struct
    {
		private readonly Dictionary<TGroupIndicator, InclusionRule> _map;

	    public GroupInclusionRuleMap()
	    {
		    _map = new Dictionary<TGroupIndicator, InclusionRule>
		    {
			    {default(TGroupIndicator), InclusionRule.Break}
		    };
	    }

	    public InclusionRule GetInclusionRuleFor(TGroupIndicator groupIndicator)
	    {
		    return _map[groupIndicator];
	    }

	    public GroupInclusionRuleMap<TGroupIndicator> Add(TGroupIndicator groupIndicator, InclusionRule rule)
	    {
		    _map.Add(groupIndicator, rule);
		    return this;
	    }
    }
}
