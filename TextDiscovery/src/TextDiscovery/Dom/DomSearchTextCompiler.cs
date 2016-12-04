
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public class DomSearchTextCompiler<TNode, TGroupIndicator> 
		: DomTextCompilerBase<TNode, TGroupIndicator, SearchTextSliceGroup<TGroupIndicator>, SearchTextSlice>, IDomSearchTextCompiler<TNode, TGroupIndicator> 
		where TGroupIndicator : struct
    {
	    private readonly ISearchTextSlicer _slicer;

	    public DomSearchTextCompiler(ISearchTextSlicer slicer,
									 IDomInterpreter<TNode> interpreter,
									 IDomGroupIndicatorMap<TNode, TGroupIndicator> groupIndicatorMap,
									 IGroupInclusionRuleMap<TGroupIndicator> groupInclusionRuleMap)
		    : base(interpreter, groupIndicatorMap, groupInclusionRuleMap)
	    {
		    _slicer = slicer;
	    }
		
	    public IEnumerable<SearchTextSliceGroup<TGroupIndicator>> Compile(TNode root, IReadOnlyList<SearchToken> searchTokens, int? maxTokenCount = null)
	    {
			return CreateSliceGroups(root, 
									 maxTokenCount ?? int.MaxValue, 
									 (text, tokenCount) => _slicer.CreateSlices(text, searchTokens, tokenCount));
	    }
		
	    protected override SearchTextSliceGroup<TGroupIndicator> GetGroupFrom(TGroupIndicator groupIndicator, IReadOnlyList<SearchTextSlice> slices)
	    {
		    return new SearchTextSliceGroup<TGroupIndicator>(groupIndicator, slices);
	    }
    }
}
