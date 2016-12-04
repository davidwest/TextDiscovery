
namespace TextDiscovery.Dom
{
    public class DomInclusionRuleMap<TNode> : IDomInclusionRuleMap<TNode>
    {
	    private readonly IDomInterpreter<TNode> _interpreter;
	    private readonly ElementInclusionRuleMap _innerMap;

	    public DomInclusionRuleMap(IDomInterpreter<TNode> interpreter, ElementInclusionRuleMap innerMap)
	    {
		    _interpreter = interpreter;
		    _innerMap = innerMap;
	    }

	    public InclusionRule? GetInclusionRuleFor(TNode node)
	    {
		    var name = _interpreter.GetName(node);
		    var rule = _innerMap.GetInclusionRuleFor(name);

		    return rule;
	    }
    }
}
