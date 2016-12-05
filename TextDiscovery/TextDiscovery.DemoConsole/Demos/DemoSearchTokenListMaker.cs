
using System.Diagnostics;

namespace TextDiscovery.DemoConsole.Demos
{
    public static class DemoSearchTokenListMaker
    {
        private const string SearchExpression = 
@"
Ads ad blaze photograph photography photographing
intellegence intellegent
I'm trying to fix something: holes in my balloons! 
Are you understanding this? 
Preparation is essential, and there are difficulties. 
Multiple responsibilities; single responsibility.
Don't make me laugh. 
Let me check my email.
Languishing. 
Excavations.
Moon's alignment with the earth; planets' Alignment with the sun.";

        public static void Start()
        {
            var searchTokenListMaker = Resolver.Get<ISearchTokenListMaker>();

			var words = searchTokenListMaker.MakeTokensFrom(SearchExpression);

			foreach (var word in words)
			{
				Debug.WriteLine(word);
			}
		}
    }
}
