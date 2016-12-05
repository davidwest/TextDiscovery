
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TextDiscovery.DemoConsole.Demos
{
    public static class DemoTextSlicer
    {
        public static void Start()
        {
            Debug.WriteLine("*** Unlimited tokens ***");
            DemoSlicing();

            Debug.WriteLine("\n\n*** Max tokens = 3 ***");
            DemoSlicing(3);
        }
        
        private static void DemoSlicing(int? maxTokenCount = null)
        {
	        var slicer = Resolver.Get<ITextSlicer>();

            var slices = slicer.CreateSlices(SourceText, maxTokenCount).ToArray();

            Display(slices);
        }
		
        private static void Display(IReadOnlyCollection<TextSlice> slices)
        {
            foreach (var slice in slices)
            {
                Debug.WriteLine($"{slice.Text,-20} {(slice.IsToken ? "*" : " "),-5} {(slice.IsOverflow ? "OVERFLOW" : "")}");
            }

            Debug.WriteLine("\n>>>>> " + slices.Where(s => s.IsToken && !s.IsOverflow).SerializeToString(s => s.Text, " "));
        }

        private const string SourceText = 
@"   Hi!!!BreAk   this//down\\in:2%tokens
    and       recogniz.e    whitespaces
!!! ... and include contractions (for example : can't, won't) or 
possessives (Somebody's dog's bone).";

    }
}
