
using System.Collections.Generic;
using System.Linq;

namespace TextDiscovery.Dom
{
    public class DomExcerpter<TNode> : IDomExcerpter<TNode>
    {
	    private readonly ITextSlicer _slicer;
	    private readonly IDomInterpreter<TNode> _interpreter;
	    private readonly IDomEllipsisNodeFactory<TNode> _ellipsisNodeFactory;
	    private readonly IDomInclusionRuleMap<TNode> _inclusionRuleMap;

	    public DomExcerpter(ITextSlicer slicer, 
							IDomInterpreter<TNode> interpreter,
							IDomInclusionRuleMap<TNode> inclusionRuleMap,
							IDomEllipsisNodeFactory<TNode> ellipsisNodeFactory)
	    {
		    _slicer = slicer;
		    _interpreter = interpreter;
		    _ellipsisNodeFactory = ellipsisNodeFactory;
		    _inclusionRuleMap = inclusionRuleMap;
	    }
		
	    public void Trim(TNode root, int maxTokenCount)
	    {
		    var allNodes = _interpreter.ToEnumerableInHierarchyOrder(root).ToArray();

		    var tokensAvailable = maxTokenCount;
			
		    var node = default(TNode);
		    List<TextSlice> slices = null;
		    TextSlice overflowToken = null;

		    var i = -1;
		    while (true)
		    {
			    i++;

			    if (i == allNodes.Length || tokensAvailable < 1) break;
				
			    node = allNodes[i];

			    if (!_interpreter.IsTextNode(node)) continue;

				var text = _interpreter.GetText(node);

				slices = _slicer.CreateSlices(text, tokensAvailable).ToList();
				
			    var tokens = slices.Where(s => s.IsToken).ToArray();

			    if (tokens.Length == 0) continue;

			    var lastToken = tokens[tokens.Length - 1];

			    if (lastToken.IsOverflow)
			    {
				    overflowToken = lastToken;
				    break;
			    }

			    tokensAvailable -= tokens.Length;
		    }
			
		    if (Equals(node, default(TNode)) || i == allNodes.Length) return;

			var pathToTextNode = GetAncestorsToRoot(root, node).Reverse();
			
			var ancestorRulePair =
				(from n in pathToTextNode
				 let rule = _inclusionRuleMap.GetInclusionRuleFor(n)
				 where rule.HasValue
				 select new { Ancestor = n, Rule = rule.Value}).FirstOrDefault();

			var leftoverNodes = 
				DetermineLeftoverNodes(ancestorRulePair != null ? ancestorRulePair.Ancestor : default(TNode), 
									   ancestorRulePair?.Rule ?? InclusionRule.Break, 
									   allNodes, i).ToArray();

			DetachNodes(leftoverNodes);

		    if (ancestorRulePair != null && ancestorRulePair.Rule != InclusionRule.Break)
		    {
			    return;
		    }

		    if (overflowToken != null)
		    {
				var textToKeep = 
					slices
					.TakeWhile(s => !s.Equals(overflowToken))
					.Select(s => s.Text)
					.SerializeToString();

				_interpreter.SetText(node, textToKeep);
			}
		    
		    var ellipsis = _ellipsisNodeFactory.CreateEllipsisNode(root);

		    if (Equals(ellipsis, default(TNode))) return;

		    var textParent = _interpreter.GetParent(node);

			_interpreter.AppendChild(textParent, ellipsis);
	    }
		

	    private IEnumerable<TNode> DetermineLeftoverNodes(TNode ancestor, InclusionRule overflowRule, TNode[] allNodes, int focalTextNodeIndex)
	    {
		    if (overflowRule == InclusionRule.Break)
		    {
			    return allNodes.Skip(focalTextNodeIndex + 1);
		    }

			var preNodes = allNodes.Take(focalTextNodeIndex + 1);
			var subTreeNodes = _interpreter.ToEnumerableInHierarchyOrder(ancestor);
			
			var leftoverNodes =
				overflowRule == InclusionRule.IncludeEntirely
					? allNodes.Except(preNodes.Union(subTreeNodes))
					: allNodes.Except(preNodes.Except(subTreeNodes));

			return leftoverNodes;
		}

	    private void DetachNodes(IReadOnlyList<TNode> nodes)
	    {
			for (var i = 0; i != nodes.Count; i++)
			{
				var node = nodes[i];

				if (!Equals(node, default(TNode)))
				{
					_interpreter.Detach(node);
				}
			}
		}
	
		private IEnumerable<TNode> GetAncestorsToRoot(TNode root, TNode node)
	    {
		    while (!node.Equals(root))
		    {
			    node = _interpreter.GetParent(node);

			    yield return node;
		    }

		    yield return root;
	    }
    }
}
