

using System;
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public class ElementGroupIndicatorMap<TGroupIndicator> where TGroupIndicator : struct
    {
	    private readonly Dictionary<string, TGroupIndicator> _map;

	    public ElementGroupIndicatorMap()
	    {
		    _map = new Dictionary<string, TGroupIndicator>(StringComparer.OrdinalIgnoreCase);
	    }

	    public TGroupIndicator GetGroupIndicatorFor(string elementName)
	    {
		    TGroupIndicator indicator;

		    _map.TryGetValue(elementName, out indicator);
		    
			return indicator;
	    }

	    public ElementGroupIndicatorMap<TGroupIndicator> Add(string elementName, TGroupIndicator groupIndicator)
	    {
		    _map.Add(elementName, groupIndicator);
		    return this;
	    }
    }
}
