
using System.Collections.Generic;

namespace TextDiscovery.Html
{
    public interface IHtmlSearchTextCompiler<TGroupIndicator> where TGroupIndicator : struct
    {
		IEnumerable<SearchTextSliceGroup<TGroupIndicator>> Compile(string html, IReadOnlyList<SearchToken> searchTokens, int? maxTokenCount = null);
	}
}
