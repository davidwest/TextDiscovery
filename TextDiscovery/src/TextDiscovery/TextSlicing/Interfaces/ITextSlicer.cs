
using System.Collections.Generic;

namespace TextDiscovery
{
    public interface ITextSlicer
    {
        IEnumerable<TextSlice> CreateSlices(string source, int? maxTokenCount = null);
    }
}
