
using System.Collections.Generic;

namespace TextDiscovery
{
    public interface ISearchTokenListMaker
    {
        IReadOnlyList<SearchToken> MakeTokensFrom(string searchExpression);
    }
}
