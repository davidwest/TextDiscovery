
namespace TextDiscovery.Html
{
    public interface IHtmlConverter<TNode>
    {
	    TNode ConvertToNode(string html);
	    string Render(TNode node);
    }
}
