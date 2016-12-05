using System.Collections.Generic;
using System.IO;
using SimpleInjector;

using TextDiscovery.Dom;


namespace TextDiscovery.DemoConsole
{
    public static class Initializer
    {
		private const string NoiseWordsPath = @"..\..\TextSource\noisewords.txt";

		public static void Initialize()
		{
			var noiseWords = File.ReadAllLines(NoiseWordsPath);
			var noiseWordSet = new NoiseWordSet(noiseWords);

		    var container = new Container();

			InitializeCommon(container, noiseWordSet);
			HtmlAgilityRegistrations.Initialize(container);
			//AngleSharpRegistrations.Initialize(container);

			container.Verify();

			Resolver.Initialize(container);
	    }

	    private static void InitializeCommon(Container container, NoiseWordSet noiseWordSet)
	    {
			container.RegisterSingleton<ITextSlicer>(TextSlicer.Default);

			container.RegisterSingleton(new TokenListMakerConfig(noiseWordSet));
			container.RegisterSingleton<ISearchTokenListMaker, SpecializedSearchTokenListMaker>();

			container.RegisterSingleton(new TokenMatchResolverConfig(noiseWordSet));
			container.RegisterSingleton<ISearchTokenMatchResolver, SearchTokenMatchResolver>();

			container.RegisterSingleton<SearchTextSlicerConfig>();
			container.RegisterSingleton<ISearchTextSlicer, SearchTextSlicer>();


			container.RegisterSingleton(new ElementGroupIndicatorMap<GroupIndicator>()
											.Add("a", GroupIndicator.Anchor)
											.Add("h1", GroupIndicator.Heading)
											.Add("h2", GroupIndicator.Heading)
											.Add("h3", GroupIndicator.Heading)
											.Add("h4", GroupIndicator.Heading));

			container.RegisterSingleton<IGroupInclusionRuleMap<GroupIndicator>>(new GroupInclusionRuleMap<GroupIndicator>()
																					.Add(GroupIndicator.Anchor, InclusionRule.IncludeEntirely)
																					.Add(GroupIndicator.Heading, InclusionRule.Exclude));

			container.RegisterSingleton(new ElementInclusionRuleMap()
											.Add("tr", InclusionRule.IncludeEntirely)
											.Add("li", InclusionRule.IncludeEntirely)
											.Add("h1", InclusionRule.Exclude)
											.Add("h2", InclusionRule.Exclude)
											.Add("h3", InclusionRule.Exclude)
											.Add("h4", InclusionRule.Exclude));


			var markAttributes = new Dictionary<string, string> { { "style", "background-color: yellow;" } };

			container.RegisterSingleton(new HtmlMarkConfig
			{
				GetAttributesFor = kind => markAttributes,
				ElementName = "span"
			});


			var ellipsisAttributes = new Dictionary<string, string> { { "style", "margin-left: 5px; background-color: lightblue; font-weight: bold;" } };

			container.RegisterSingleton(new HtmlEllipsisConfig
			{
				Attributes = ellipsisAttributes
			});
		}
    }
}
