
using HtmlAgilityPack;
using SimpleInjector;

using TextDiscovery.Dom;
using TextDiscovery.Dom.HtmlAgilityPack;
using TextDiscovery.Html;
using TextDiscovery.Html.HtmlAgilityPack;


namespace TextDiscovery.DemoConsole
{
	public static class HtmlAgilityRegistrations
	{
		public static void Initialize(Container container)
		{
			container.RegisterSingleton<IDomInterpreter<HtmlNode>, DomInterpreter>();
			container.RegisterSingleton<IHtmlConverter<HtmlNode>, HtmlConverter>();

			var nodeFactoryRegistration = Lifestyle.Singleton.CreateRegistration<DomNodeFactory>(container);
			container.AddRegistration(typeof(IDomTextNodeFactory<HtmlNode>), nodeFactoryRegistration);
			container.AddRegistration(typeof(IDomElementNodeFactory<HtmlNode>), nodeFactoryRegistration);

			// ----------------------------------------------------------------------------------------------------------------------
			container.RegisterSingleton<IDomGroupIndicatorMap<HtmlNode, GroupIndicator>, DomGroupIndicatorMap<HtmlNode, GroupIndicator>>();

			container.RegisterSingleton<IDomTextCompiler<HtmlNode, GroupIndicator>, DomTextCompiler<HtmlNode, GroupIndicator>>();
			container.RegisterSingleton<IHtmlTextCompiler, HtmlTextCompiler<HtmlNode>>();

			container.RegisterSingleton<IDomSearchTextCompiler<HtmlNode, GroupIndicator>, DomSearchTextCompiler<HtmlNode, GroupIndicator>>();
			container.RegisterSingleton<IHtmlSearchTextCompiler, HtmlSearchTextCompiler<HtmlNode>>();

			// ---------------------------------------------------------------------------------------------------------------------------------------------			
			container.RegisterSingleton<IDomMarkerNodeFactory<HtmlNode>, DomMarkerNodeFactory<HtmlNode>>();
			container.RegisterSingleton<IDomSearchTextMarker<HtmlNode>, DomSearchTextMarker<HtmlNode>>();
			container.RegisterSingleton<IHtmlSearchTextMarker, HtmlSearchTextMarker<HtmlNode>>();

			// ---------------------------------------------------------------------------------------------------------------------------------------------
			container.RegisterSingleton<IDomInclusionRuleMap<HtmlNode>, DomInclusionRuleMap<HtmlNode>>();
			container.RegisterSingleton<IDomEllipsisNodeFactory<HtmlNode>, DomEllipsisNodeFactory<HtmlNode>>();
			container.RegisterSingleton<IDomExcerpter<HtmlNode>, DomExcerpter<HtmlNode>>();
			container.RegisterSingleton<IHtmlExcerpter, HtmlExcerpter<HtmlNode>>();
		}
	}
}
