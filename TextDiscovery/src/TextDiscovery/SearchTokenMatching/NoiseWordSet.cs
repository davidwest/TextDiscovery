
using System;
using System.Collections.Generic;

namespace TextDiscovery
{
    public class NoiseWordSet
    {
	    private readonly HashSet<string> _noiseWords;

	    public NoiseWordSet(IEnumerable<string> noiseWords)
	    {
			_noiseWords = new HashSet<string>(noiseWords, StringComparer.OrdinalIgnoreCase);    
	    }

	    public bool Contains(string token) => _noiseWords.Contains(token);

	    public bool Contains(char[] token) => _noiseWords.Contains(new string(token));
    }
}
