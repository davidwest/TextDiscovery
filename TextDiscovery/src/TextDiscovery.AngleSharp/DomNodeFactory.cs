
using System.Collections.Generic;
using AngleSharp.Dom;

using TextDiscovery.Dom;


namespace TextDiscovery.AngleSharp.Dom
{
    public class DomNodeFactory : IDomNodeFactory<INode>
    {
		private static readonly Dictionary<string, string> EmptyDictionary = new Dictionary<string, string>();

		public INode CreateTextNode(INode root, string text)
	    {
		    return root.Owner.CreateTextNode(text);
	    }
		
	    public INode CreateElementNode(INode root, string elementName, Dictionary<string, string> attributes, string innerHtml = null)
		{
			var node = root.Owner.CreateElement(elementName);

			foreach (var attribute in attributes)
			{
				node.SetAttribute(attribute.Key, attribute.Value);
			}

			if (innerHtml != null)
			{
				node.InnerHtml = innerHtml;
			}

			return node;
		}
		
	    public INode CreateElementNode(INode root, string elementName, string innerHtml = null)
	    {
		    return CreateElementNode(root, elementName, EmptyDictionary, innerHtml);
	    }
    }
}
