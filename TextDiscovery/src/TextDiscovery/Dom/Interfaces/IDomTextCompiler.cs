
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public interface IDomTextCompiler<in TNode, TGroupIndicator> where TGroupIndicator : struct
    {
        IEnumerable<TextSliceGroup<TGroupIndicator>> Compile(TNode root, int? maxTokenCount = null);
    }
}
