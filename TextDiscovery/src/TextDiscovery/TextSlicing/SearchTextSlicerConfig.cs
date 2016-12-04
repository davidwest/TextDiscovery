
using System;
using System.Linq;

namespace TextDiscovery
{
    public class SearchTextSlicerConfig : TextSlicerConfig
    {
	    public Func<char[], bool> RequiresExactMatch { get; } = chars => chars.Any(c => c.IsNumeric());
    }
}
