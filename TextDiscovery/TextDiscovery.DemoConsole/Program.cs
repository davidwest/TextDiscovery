
using System;
using System.Diagnostics;

namespace TextDiscovery.DemoConsole
{
	public static class Program
	{
		public static void Main()
		{
			Console.WriteLine(@"*** Initializing ***");
			Initializer.Initialize();

			Console.WriteLine(@"*** STARTED DEMO ***");
			Console.WriteLine();

			var sw = new Stopwatch();
			sw.Start();

			DemoCanvas.Start();

			sw.Stop();

			Console.WriteLine();
			Console.WriteLine("*** FINISHED DEMO : {0} ***", sw.Elapsed);

			Console.ReadKey();
		}
	}
}
