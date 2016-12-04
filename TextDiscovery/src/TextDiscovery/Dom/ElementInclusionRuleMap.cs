
using System;
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public class ElementInclusionRuleMap
    {
	    private readonly Dictionary<string, InclusionRule> _innerMap;

		public ElementInclusionRuleMap()
		{
			_innerMap = new Dictionary<string, InclusionRule>(StringComparer.OrdinalIgnoreCase);
		}

	    public InclusionRule? GetInclusionRuleFor(string elementName)
	    {
		    InclusionRule rule;

		    if (_innerMap.TryGetValue(elementName, out rule))
		    {
			    return rule;
		    }

		    return null;
	    }

	    public ElementInclusionRuleMap Add(string elementName, InclusionRule rule)
	    {
		    _innerMap.Add(elementName, rule);
		    return this;
	    }
    }
}
