
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
	public interface IDomSearchTextCompiler<in TNode, TGroupIndicator> where TGroupIndicator : struct
    {
        IEnumerable<SearchTextSliceGroup<TGroupIndicator>> Compile(TNode root, IReadOnlyList<SearchToken> searchTokens, int? maxTokenCount = null);
    }
}
