
using SimpleInjector;

namespace TextDiscovery.DemoConsole
{
    public static class Resolver
    {
	    private static Container _container;

	    public static void Initialize(Container container)
	    {
		    _container = container;
	    }

	    public static T Get<T>() where T : class
	    {
		    return _container.GetInstance<T>();
	    }
    }
}
