
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace TextDiscovery.Dom.HtmlAgilityPack
{
	public class DomInterpreter : IDomInterpreter<HtmlNode>
	{
		public IEnumerable<HtmlNode> ToEnumerableInHierarchyOrder(HtmlNode node)
		{
			return node.DescendantsAndSelf();
		}

		public IEnumerable<HtmlNode> GetTextNodesInHierarchyOrder(HtmlNode node)
		{
			return node.DescendantsAndSelf().Where(n => n.NodeType == HtmlNodeType.Text);
		}

		public string GetName(HtmlNode node) => node.Name;

		public bool IsTextNode(HtmlNode node) => node.NodeType == HtmlNodeType.Text;
		public string GetText(HtmlNode node) => HttpUtility.HtmlDecode(node.InnerText);
		public void SetText(HtmlNode node, string text) => node.InnerHtml = text;

		public HtmlNode GetParent(HtmlNode node) => node.ParentNode;
		public IEnumerable<HtmlNode> GetChildren(HtmlNode node) => node.ChildNodes;

		public void Detach(HtmlNode node) => node.ParentNode.RemoveChild(node);
		public void AppendChild(HtmlNode parent, HtmlNode child) => parent.AppendChild(child);
	}
}
