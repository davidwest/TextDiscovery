
using System;

namespace TextDiscovery
{
    public class TextSlicerConfig
    {
	    public Func<char, bool> IsPartOfToken { get; } = c => c.IsAlphaNumericOrApostrophe();
    }
}
