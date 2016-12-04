
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextDiscovery.Dom
{
    public abstract class DomTextCompilerBase<TNode, TGroupIndicator, TGroup, TSlice> 
        where TSlice : TextSliceBase
        where TGroup : TextSliceGroupBase<TGroupIndicator, TSlice>
		where TGroupIndicator : struct
    {
		private static readonly TGroupIndicator DefaultGroupIndicator = default(TGroupIndicator);

		private readonly IDomInterpreter<TNode> _interpreter;
        private readonly IDomGroupIndicatorMap<TNode, TGroupIndicator> _groupIndicatorMap;
	    private readonly IGroupInclusionRuleMap<TGroupIndicator> _groupInclusionRuleMap;

        protected DomTextCompilerBase(IDomInterpreter<TNode> interpreter, 
                                      IDomGroupIndicatorMap<TNode, TGroupIndicator> groupIndicatorMap,
									  IGroupInclusionRuleMap<TGroupIndicator> groupInclusionRuleMap)
        {
            _interpreter = interpreter;
            _groupIndicatorMap = groupIndicatorMap;
	        _groupInclusionRuleMap = groupInclusionRuleMap;
        }
        
        protected abstract TGroup GetGroupFrom(TGroupIndicator groupIndicator, IReadOnlyList<TSlice> slices);

	    protected virtual bool IsMatch(TGroupIndicator i1, TGroupIndicator i2) => i1.Equals(i2);
	    
		
        protected IEnumerable<TGroup> CreateSliceGroups(TNode root, 
                                                        int maxTokenCount, 
                                                        Func<string, int, IEnumerable<TSlice>> getSlices)
        {
			var tokensAvailable = maxTokenCount;

			var rootGroupIndicator = _groupIndicatorMap.GetGroupIndicatorFor(root);

	        IReadOnlyList<TSlice> slices;

			if (!IsMatch(rootGroupIndicator, DefaultGroupIndicator))
			{
				// root is well-defined group; yield and stop here

				var text = _interpreter.GetText(root);

				slices = GetEffectiveSlices(rootGroupIndicator, text, ref tokensAvailable, getSlices);

				if (slices == null) yield break;

				if (slices.Count != 0)
				{
					yield return GetGroupFrom(rootGroupIndicator, slices);
				}
				
				yield break;
			}

			// root is undefined group; proceed with normal processing:

			var textNodes = _interpreter.GetTextNodesInHierarchyOrder(root);

            var builder = new StringBuilder();

            var commonGroupIndicator = DefaultGroupIndicator;
            var commonNodeAncestor = root;

            foreach (var tnode in textNodes)
            {
                if (tokensAvailable < 1) yield break;

                var text = _interpreter.GetText(tnode).Trim();

                if (string.IsNullOrWhiteSpace(text)) continue;

                var topGroupIndicatorInPath = DefaultGroupIndicator;
                var topAncestorInPath = root;

                var n = tnode;
                while (true)
                {
                    n = _interpreter.GetParent(n);

                    if (n.Equals(root)) break;
                    
                    var groupIndicator = _groupIndicatorMap.GetGroupIndicatorFor(n);

                    if (IsMatch(groupIndicator, DefaultGroupIndicator)) continue;

                    topGroupIndicatorInPath = groupIndicator;
                    topAncestorInPath = n;
                }

                if (topAncestorInPath.Equals(commonNodeAncestor))
                {
					// keep on building!
                    builder.Append($"{text} ");
                    continue;
                }

				// stop building and yield:

	            slices = GetEffectiveSlices(commonGroupIndicator, builder.ToString(), ref tokensAvailable, getSlices);

	            if (slices == null) yield break;

	            if (slices.Count != 0)
	            {
		            yield return GetGroupFrom(commonGroupIndicator, slices);
	            }

				// reset all
	            builder.Clear().Append($"{text} ");
	            commonGroupIndicator = topGroupIndicatorInPath;
	            commonNodeAncestor = topAncestorInPath;
            }

			// handle leftovers in builder:

            if (builder.Length == 0) yield break;

            slices = GetEffectiveSlices(commonGroupIndicator, builder.ToString(), ref tokensAvailable, getSlices);

	        if (slices == null) yield break;

	        if (slices.Count != 0)
	        {
				yield return GetGroupFrom(commonGroupIndicator, slices);
			}
        }

		
		private IReadOnlyList<TSlice> GetEffectiveSlices(TGroupIndicator groupIndicator,
														 string text,
														 ref int tokensAvailable,
														 Func<string, int, IEnumerable<TSlice>> getSlices)
		{
			var rule = _groupInclusionRuleMap.GetInclusionRuleFor(groupIndicator);

			var effectiveTokenCount = rule == InclusionRule.IncludeEntirely
					? int.MaxValue
					: tokensAvailable;

			var slices = getSlices(text, effectiveTokenCount).ToList();
			
			tokensAvailable -= slices.Count(s => s.IsToken);

			if (tokensAvailable > 0) return slices;

			return rule == InclusionRule.Exclude ? null : slices;
		}
	}
}
