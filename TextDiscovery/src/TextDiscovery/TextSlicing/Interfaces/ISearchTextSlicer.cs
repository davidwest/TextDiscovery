
using System.Collections.Generic;

namespace TextDiscovery
{
    public interface ISearchTextSlicer
    {
        IEnumerable<SearchTextSlice> CreateSlices(string source, IReadOnlyList<SearchToken> searchTokens, int? maxTokenCount = null);
    }
}
