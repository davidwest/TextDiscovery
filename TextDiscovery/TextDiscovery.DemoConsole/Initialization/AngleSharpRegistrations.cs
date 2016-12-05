
using AngleSharp.Dom;
using SimpleInjector;

using TextDiscovery.Dom;
using TextDiscovery.AngleSharp.Dom;
using TextDiscovery.Html;
using TextDiscovery.AngleSharp.Html;


namespace TextDiscovery.DemoConsole
{
	public static class AngleSharpRegistrations
	{
		public static void Initialize(Container container)
		{
			container.RegisterSingleton<IDomInterpreter<INode>, DomInterpreter>();
			container.RegisterSingleton<IHtmlConverter<INode>, HtmlConverter>();

			var nodeFactoryRegistration = Lifestyle.Singleton.CreateRegistration<DomNodeFactory>(container);
			container.AddRegistration(typeof(IDomTextNodeFactory<INode>), nodeFactoryRegistration);
			container.AddRegistration(typeof(IDomElementNodeFactory<INode>), nodeFactoryRegistration);

			// ----------------------------------------------------------------------------------------------------------------------
			container.RegisterSingleton<IDomGroupIndicatorMap<INode, GroupIndicator>, DomGroupIndicatorMap<INode, GroupIndicator>>();

			container.RegisterSingleton<IDomTextCompiler<INode, GroupIndicator>, DomTextCompiler<INode, GroupIndicator>>();
			container.RegisterSingleton<IHtmlTextCompiler, HtmlTextCompiler<INode>>();

			container.RegisterSingleton<IDomSearchTextCompiler<INode, GroupIndicator>, DomSearchTextCompiler<INode, GroupIndicator>>();
			container.RegisterSingleton<IHtmlSearchTextCompiler, HtmlSearchTextCompiler<INode>>();

			// ---------------------------------------------------------------------------------------------------------------------------------------------
			container.RegisterSingleton<IDomMarkerNodeFactory<INode>, DomMarkerNodeFactory<INode>>();
			container.RegisterSingleton<IDomSearchTextMarker<INode>, DomSearchTextMarker<INode>>();
			container.RegisterSingleton<IHtmlSearchTextMarker, HtmlSearchTextMarker<INode>>();

			// ---------------------------------------------------------------------------------------------------------------------------------------------
			container.RegisterSingleton<IDomInclusionRuleMap<INode>, DomInclusionRuleMap<INode>>();
			container.RegisterSingleton<IDomEllipsisNodeFactory<INode>, DomEllipsisNodeFactory<INode>>();
			container.RegisterSingleton<IDomExcerpter<INode>, DomExcerpter<INode>>();
			container.RegisterSingleton<IHtmlExcerpter, HtmlExcerpter<INode>>();
		}
	}
}
