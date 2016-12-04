
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextDiscovery.Dom
{
    public class DomSearchTextMarker<TNode> : IDomSearchTextMarker<TNode>
    {
	    private readonly ISearchTextSlicer _slicer;
	    private readonly IDomInterpreter<TNode> _interpreter;
	    private readonly IDomTextNodeFactory<TNode> _nodeFactory;
	    private readonly IDomMarkerNodeFactory<TNode> _markerNodeFactory;

	    public DomSearchTextMarker(ISearchTextSlicer slicer, 
								   IDomInterpreter<TNode> interpreter,
								   IDomTextNodeFactory<TNode> nodeFactory,
								   IDomMarkerNodeFactory<TNode> markerNodeFactory)
	    {
		    _slicer = slicer;
		    _interpreter = interpreter;
		    _nodeFactory = nodeFactory;
		    _markerNodeFactory = markerNodeFactory;
	    }
		
	    public void MarkSearchMatches(TNode root, IReadOnlyList<SearchToken> searchTokens)
	    {
			var allNodes = _interpreter.ToEnumerableInHierarchyOrder(root).ToArray();
			
		    for (var i = 0; i != allNodes.Length; i ++)
		    {
			    var node = allNodes[i];

			    if (!_interpreter.IsTextNode(node)) continue;

				var text = _interpreter.GetText(node);

				var slices = _slicer.CreateSlices(text, searchTokens).ToArray();

			    if (slices.All(s => s.Match == null)) continue;
				
			    var parent = _interpreter.GetParent(node);

			    var children = _interpreter.GetChildren(parent).ToList();

				children.ForEach(c => _interpreter.Detach(c));

			    var indexOfText = children.IndexOf(node);

			    var preNodes = children.Take(indexOfText);
			    var postNodes = children.Skip(indexOfText + 1);

				preNodes.ForEach(n => _interpreter.AppendChild(parent, n));

				var segments = CondenseToTextSegments(slices);

				foreach (var seg in segments)
			    {
				    var textNode = _nodeFactory.CreateTextNode(root, seg.Text);

					if (seg.MatchKind.HasValue)
					{
						var markerNode = _markerNodeFactory.CreateMarkerNode(root, seg.MatchKind.Value);

						_interpreter.AppendChild(parent, markerNode);
						_interpreter.AppendChild(markerNode, textNode);
					}
					else
					{
						_interpreter.AppendChild(parent, textNode);
					}    
			    }

			    postNodes.ForEach(n => _interpreter.AppendChild(parent, n));
		    }
		}
		

	    private static IEnumerable<TextSegment> CondenseToTextSegments(IEnumerable<SearchTextSlice> slices)
	    {
			var builder = new StringBuilder();

		    foreach (var slice in slices)
		    {
			    var match = slice.Match;

			    if (match == null)
			    {
				    builder.Append(slice.Text);
				    continue;
			    }
		
			    if (builder.Length != 0)
			    {
				    yield return new TextSegment(builder.ToString());
			    }
			    
			    yield return new TextSegment(slice.Text, slice.Match.Kind);

			    builder.Clear();
		    }

		    if (builder.Length == 0) yield break;

		    yield return new TextSegment(builder.ToString());
	    }
			

	    internal class TextSegment
	    {
		    public TextSegment(string text, SearchTokenMatchKind? matchKind = null)
		    {
			    Text = text;
			    MatchKind = matchKind;
		    }

		    public string Text { get; }
			public SearchTokenMatchKind? MatchKind { get; }
	    }
    }
}
