
using System.Collections.Generic;

namespace TextDiscovery.Html
{
    public interface IHtmlSearchTextMarker
    {
	    string MarkSearchMatches(string html, IReadOnlyList<SearchToken> searchTokens);
    }
}
