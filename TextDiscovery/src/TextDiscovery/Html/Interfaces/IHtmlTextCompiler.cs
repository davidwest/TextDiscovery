
using System.Collections.Generic;

namespace TextDiscovery.Html
{
    public interface IHtmlTextCompiler<TGroupIndicator> where TGroupIndicator : struct
    {
		IEnumerable<TextSliceGroup<TGroupIndicator>> Compile(string html, int? maxTokenCount = null);
	}
}
