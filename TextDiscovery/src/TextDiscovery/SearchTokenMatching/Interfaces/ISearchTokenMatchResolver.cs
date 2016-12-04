
using System.Collections.Generic;

namespace TextDiscovery
{
    public interface ISearchTokenMatchResolver
    {
        SearchTokenMatch TryResolveMatch(char[] token, IReadOnlyList<SearchToken> searchTokens, bool requireExactMatch = false);
    }
}

