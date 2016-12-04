
using System;
using System.Collections.Generic;

namespace TextDiscovery
{
    public class SearchTextSlicer : TextSlicerBase<SearchTextSlice>, ISearchTextSlicer
    {
	    private readonly ISearchTokenMatchResolver _resolver;
        private readonly Func<char[], bool> _requiresExactMatch;

		public SearchTextSlicer(ISearchTokenMatchResolver resolver, SearchTextSlicerConfig config) 
            : base(config)
        {
            _resolver = resolver;
            _requiresExactMatch = config.RequiresExactMatch;
        }

		public static SearchTextSlicer Default { get; } = new SearchTextSlicer(SearchTokenMatchResolver.Default, new SearchTextSlicerConfig());


	    public IEnumerable<SearchTextSlice> CreateSlices(string source, IReadOnlyList<SearchToken> searchTokens, int? maxTokenCount = null)
        {
            var slices = 
                CreateSlices(source.ToCharArray(), 
                             maxTokenCount ?? int.MaxValue, 
                             (positionInSource, value, isToken, isOverflow) => CreateSlice(positionInSource, value, isToken, isOverflow, searchTokens));

            return slices;
        }
                
        private SearchTextSlice CreateSlice(int positionInSource, char[] token, bool isToken, bool isOverflow, IReadOnlyList<SearchToken> searchTokens)
        {
            var match = isToken 
                ? _resolver.TryResolveMatch(token, searchTokens, _requiresExactMatch(token)) 
                : null;

            return new SearchTextSlice(positionInSource, token, isToken, isOverflow, match);
        }
    }
}
