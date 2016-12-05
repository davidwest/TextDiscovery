

using System.Diagnostics;
using System.Linq;
using System.Text;


namespace TextDiscovery.DemoConsole.Demos
{
	public static class DemoHtmlTextCompiler
	{
		private static IHtmlTextCompiler _compiler;

		public static void Start()
		{
			_compiler = Resolver.Get<IHtmlTextCompiler>();

			Demo();
			Demo(7);
			Demo(16);
			Demo(17);
			Demo(18);
			Demo(22);
			Demo(26);
			Demo(30);
			Demo(36);
			Demo(37);
			Demo(38);
		}

		private static void Demo(int? maxTokenCount = null)
		{
			Debug.WriteLine($"\n\n*** Max token count : {(maxTokenCount.HasValue ? maxTokenCount.ToString() : "NO LIMIT")}");

			var groups = _compiler.Compile(SourceHtml, maxTokenCount).ToArray();

			var builder = new StringBuilder();

			foreach (var grp in groups)
			{
				builder.AppendLine($"\n--- {grp.GroupIndicator} ---");

				foreach (var slice in grp.Slices)
				{
					builder.Append(slice.IsToken ? $"[{slice.Text}{(slice.IsOverflow ? " !!! OVERFLOW !!!" : "")}]" : slice.Text);
				}
			}

			Debug.WriteLine(builder.ToString());
		}


		private const string SourceHtml =
@"
First thing's first.

<a href='#'><h1>Hey look, a heading inside an anchor!</h1></a>
<h1><a href='#'>Hey look, an anchor inside a heading!</a></h1>

<div>
    <span>Some stuff 1</span>|<span>Some stuff 2</span>
</div>
<div>
    <h2>A lesser heading</h2>
    Say <span>anything</span> at all.
    <p>
        Here's a paragraph.
    </p>
</div>
Almost the end.
<div>
    <a href='#'>Link to nowhere...</a>
</div>
The end.
";
	}
}
