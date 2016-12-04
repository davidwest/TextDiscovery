
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
    public interface IDomInterpreter<TNode>
    {
		IEnumerable<TNode> ToEnumerableInHierarchyOrder(TNode node);
		IEnumerable<TNode> GetTextNodesInHierarchyOrder(TNode node);

	    string GetName(TNode node);
		
		bool IsTextNode(TNode node);
		string GetText(TNode node);
	    void SetText(TNode node, string text);
	    
		TNode GetParent(TNode node);
	    IEnumerable<TNode> GetChildren(TNode node);
		
	    void Detach(TNode node);
	    void AppendChild(TNode parent, TNode child);
    }
}
