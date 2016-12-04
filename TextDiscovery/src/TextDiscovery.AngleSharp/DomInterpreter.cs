
using System.Collections.Generic;
using System.Linq;
using AngleSharp.Dom;
using AngleSharp.Extensions;

using TextDiscovery.Dom;


namespace TextDiscovery.AngleSharp.Dom
{
	public class DomInterpreter : IDomInterpreter<INode>
	{
		public IEnumerable<INode> ToEnumerableInHierarchyOrder(INode node)
		{
			return new[] {node}.Concat(node.Descendents());
		}

		public IEnumerable<INode> GetTextNodesInHierarchyOrder(INode node)
		{
			if (node is IText) return new[] {node};

			return node.Descendents<IText>();
		}

		public string GetName(INode node) => node.NodeName;

		public bool IsTextNode(INode node) => node is IText;
		public string GetText(INode node) => node.TextContent;
		public void SetText(INode node, string text) => node.TextContent = text;

		public INode GetParent(INode node) => node.Parent;
		public IEnumerable<INode> GetChildren(INode parent) => parent.ChildNodes;

		public void Detach(INode node) => node.Parent?.RemoveChild(node);
		public void AppendChild(INode parent, INode child) => parent.AppendChild(child);
	}
}
