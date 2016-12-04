
using System;

namespace TextDiscovery
{
    public class TokenListMakerConfig
    {
	    public TokenListMakerConfig()
	    {
		    IsValidToken = str => true;
	    }

	    public TokenListMakerConfig(Func<string, bool> isValidToken)
	    {
		    IsValidToken = isValidToken;
	    }

	    public TokenListMakerConfig(NoiseWordSet noiseWords)
	    {
		    IsValidToken = token => !noiseWords.Contains(token);
	    }

		public int MinTokenLength { get; set; } = 2;

		public Func<string, bool> IsValidToken { get; }
}
}
