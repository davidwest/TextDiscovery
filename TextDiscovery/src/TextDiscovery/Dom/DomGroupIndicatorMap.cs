
namespace TextDiscovery.Dom
{
    public class DomGroupIndicatorMap<TNode, TGroupIndicator> : IDomGroupIndicatorMap<TNode, TGroupIndicator> where TGroupIndicator : struct
    {
	    private readonly IDomInterpreter<TNode> _interpreter;
	    private readonly ElementGroupIndicatorMap<TGroupIndicator> _innerMap;

	    public DomGroupIndicatorMap(IDomInterpreter<TNode> interpreter, ElementGroupIndicatorMap<TGroupIndicator> innerMap)
	    {
		    _interpreter = interpreter;
		    _innerMap = innerMap;
	    }

	    public TGroupIndicator GetGroupIndicatorFor(TNode node)
	    {
		    var name = _interpreter.GetName(node);
		    var rule = _innerMap.GetGroupIndicatorFor(name);

		    return rule;
	    }
    }
}
