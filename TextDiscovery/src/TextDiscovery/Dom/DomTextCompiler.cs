
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public class DomTextCompiler<TNode, TGroupIndicator> 
		: DomTextCompilerBase<TNode, TGroupIndicator, TextSliceGroup<TGroupIndicator>, TextSlice>, IDomTextCompiler<TNode, TGroupIndicator> 
		where TGroupIndicator : struct
    {
	    private readonly ITextSlicer _slicer;

	    public DomTextCompiler(ITextSlicer slicer,
							   IDomInterpreter<TNode> interpreter,
							   IDomGroupIndicatorMap<TNode, TGroupIndicator> groupIndicatorMap,
							   IGroupInclusionRuleMap<TGroupIndicator> groupInclusionRuleMap)
		    : base(interpreter, groupIndicatorMap, groupInclusionRuleMap)
	    {
		    _slicer = slicer;
	    }
		
	    public IEnumerable<TextSliceGroup<TGroupIndicator>> Compile(TNode root, int? maxTokenCount = null)
	    {
			return CreateSliceGroups(root, 
									 maxTokenCount ?? int.MaxValue, 
									 (text, tokenCount) => _slicer.CreateSlices(text, tokenCount));
	    }

	    protected override TextSliceGroup<TGroupIndicator> GetGroupFrom(TGroupIndicator groupIndicator, IReadOnlyList<TextSlice> slices)
	    {
		    return new TextSliceGroup<TGroupIndicator>(groupIndicator, slices);
	    }
    }
}
