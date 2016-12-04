
using System;

namespace TextDiscovery
{
    public class TokenMatchResolverConfig
    {
	    public TokenMatchResolverConfig()
	    {
		    IsValidToken = chars => true;
	    }

	    public TokenMatchResolverConfig(Func<char[], bool> isValidToken)
	    {
		    IsValidToken = isValidToken;
	    }

	    public TokenMatchResolverConfig(NoiseWordSet noiseWords)
	    {
		    IsValidToken = token => !noiseWords.Contains(token);
	    }

	    public int MinTokenLength { get; set; } = 2;
	    public int MinStartMatchLength { get; set; } = 4;
	    public bool IgnoreContains { get; set; } = true;
		
	    public Func<char, char, bool> CharEquals { get; set; } = (c1, c2) => c1.CaseInsensitiveEquals(c2);

	    public Func<char[], bool> IsValidToken { get; } 
    }
}
