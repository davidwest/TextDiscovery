
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public interface IDomSearchTextMarker<in TNode>
    {
	    void MarkSearchMatches(TNode root, IReadOnlyList<SearchToken> searchTokens);
    }
}
