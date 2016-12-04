using System.Collections.Generic;
using System.IO;
using AngleSharp.Dom;
using SimpleInjector;

using TextDiscovery.Dom;
using TextDiscovery.Html;
using TextDiscovery.AngleSharp.Dom;
using TextDiscovery.AngleSharp.Html;


namespace TextDiscovery.DemoConsole
{
    public static class Initializer
    {
		private const string NoiseWordsPath = @"TextSource\noisewords.txt";

		public static void Initialize()
		{
			var noiseWords = File.ReadAllLines(NoiseWordsPath);
			var noiseWordSet = new NoiseWordSet(noiseWords);

		    var container = new Container();

			Register(container, noiseWordSet);

			container.Verify();

			Resolver.Initialize(container);
	    }

	    private static void Register(Container container, NoiseWordSet noiseWordSet)
	    {
			container.RegisterSingleton<ITextSlicer>(TextSlicer.Default);

			container.RegisterSingleton(new TokenListMakerConfig(noiseWordSet));
			container.RegisterSingleton<ISearchTokenListMaker, SpecializedSearchTokenListMaker>();

			container.RegisterSingleton(new TokenMatchResolverConfig(noiseWordSet));
			container.RegisterSingleton<ISearchTokenMatchResolver, SearchTokenMatchResolver>();

			container.RegisterSingleton<SearchTextSlicerConfig>();
			container.RegisterSingleton<ISearchTextSlicer, SearchTextSlicer>();


			// ----------------------------------------------------------------------------------------------------------------------
			container.RegisterSingleton<IDomInterpreter<INode>, DomInterpreter>();
			container.RegisterSingleton<IHtmlConverter<INode>, HtmlConverter>();

			var nodeFactoryRegistration = Lifestyle.Singleton.CreateRegistration<DomNodeFactory>(container);
			container.AddRegistration(typeof(IDomTextNodeFactory<INode>), nodeFactoryRegistration);
			container.AddRegistration(typeof(IDomElementNodeFactory<INode>), nodeFactoryRegistration);
			// ----------------------------------------------------------------------------------------------------------------------

			container.RegisterSingleton(new ElementGroupIndicatorMap<GroupIndicator>()
											.Add("a", GroupIndicator.Anchor)
											.Add("h1", GroupIndicator.Heading)
											.Add("h2", GroupIndicator.Heading)
											.Add("h3", GroupIndicator.Heading)
											.Add("h4", GroupIndicator.Heading));

			container.RegisterSingleton<IDomGroupIndicatorMap<INode, GroupIndicator>, DomGroupIndicatorMap<INode, GroupIndicator>>();

			container.RegisterSingleton<IGroupInclusionRuleMap<GroupIndicator>>(new GroupInclusionRuleMap<GroupIndicator>()
																					.Add(GroupIndicator.Anchor, InclusionRule.IncludeEntirely)
																					.Add(GroupIndicator.Heading, InclusionRule.Exclude));

			container.RegisterSingleton<IDomTextCompiler<INode, GroupIndicator>, DomTextCompiler<INode, GroupIndicator>>();
			container.RegisterSingleton<IHtmlTextCompiler, HtmlTextCompiler<INode>>();

			container.RegisterSingleton<IDomSearchTextCompiler<INode, GroupIndicator>, DomSearchTextCompiler<INode, GroupIndicator>>();
			container.RegisterSingleton<IHtmlSearchTextCompiler, HtmlSearchTextCompiler<INode>>();

			// ---------------------------------------------------------------------------------------------------------------------------------------------


			var markAttributes = new Dictionary<string, string> { { "style", "background-color: yellow;" } };

			container.RegisterSingleton(new HtmlMarkConfig
			{
				GetAttributesFor = kind => markAttributes
			});
			container.RegisterSingleton<IDomMarkerNodeFactory<INode>, DomMarkerNodeFactory<INode>>();
			container.RegisterSingleton<IDomSearchTextMarker<INode>, DomSearchTextMarker<INode>>();
			container.RegisterSingleton<IHtmlSearchTextMarker, HtmlSearchTextMarker<INode>>();


			// ---------------------------------------------------------------------------------------------------------------------------------------------

		    var ellipsisAttributes = new Dictionary<string, string> { { "style", "margin-left: 5px; background-color: lightblue; font-weight: bold;" } };

			container.RegisterSingleton(new HtmlEllipsisConfig
			{
				Attributes = ellipsisAttributes
			});

			container.RegisterSingleton(new ElementInclusionRuleMap()
											.Add("tr", InclusionRule.IncludeEntirely)
											.Add("li", InclusionRule.IncludeEntirely)
											.Add("h1", InclusionRule.Exclude)
											.Add("h2", InclusionRule.Exclude)
											.Add("h3", InclusionRule.Exclude)
											.Add("h4", InclusionRule.Exclude));

			container.RegisterSingleton<IDomInclusionRuleMap<INode>, DomInclusionRuleMap<INode>>();
			container.RegisterSingleton<IDomEllipsisNodeFactory<INode>, DomEllipsisNodeFactory<INode>>();
			container.RegisterSingleton<IDomExcerpter<INode>, DomExcerpter<INode>>();
			container.RegisterSingleton<IHtmlExcerpter, HtmlExcerpter<INode>>();
		}
    }
}
