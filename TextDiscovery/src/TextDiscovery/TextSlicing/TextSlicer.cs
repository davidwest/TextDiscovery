
using System.Collections.Generic;

namespace TextDiscovery
{    
    public class TextSlicer : TextSlicerBase<TextSlice>, ITextSlicer
    {
	    public TextSlicer(TextSlicerConfig config) : base(config)
        { }

		public static TextSlicer Default { get; } = new TextSlicer(new SearchTextSlicerConfig());


	    public IEnumerable<TextSlice> CreateSlices(string source, int? maxTokenCount = null)
        {
            return CreateSlices(source.ToCharArray(), maxTokenCount ?? int.MaxValue, CreateSlice);
        }
        
        private static TextSlice CreateSlice(int positionInSource, char[] characters, bool isToken, bool isOverflow)
        {
            return new TextSlice(positionInSource, characters, isToken, isOverflow);
        }
    }
}
