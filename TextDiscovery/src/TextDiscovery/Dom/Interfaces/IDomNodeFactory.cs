
using System.Collections.Generic;

namespace TextDiscovery.Dom
{
	public interface IDomTextNodeFactory<TNode>
	{
		TNode CreateTextNode(TNode root, string text);
	}

	public interface IDomElementNodeFactory<TNode>
	{
		TNode CreateElementNode(TNode root, string elementName, Dictionary<string, string> attributes, string innerHtml = null);
		TNode CreateElementNode(TNode root, string elementName, string innerHtml = null);
	}

    public interface IDomNodeFactory<TNode> : IDomTextNodeFactory<TNode>, IDomElementNodeFactory<TNode>
    { }
}
