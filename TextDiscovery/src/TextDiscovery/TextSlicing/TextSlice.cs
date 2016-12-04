
namespace TextDiscovery
{
    public abstract class TextSliceBase
    {
        protected TextSliceBase(int positionInSource, char[] charArray, bool isToken, bool isOverflow)
        {
            PositionInSource = positionInSource;
            Text = new string(charArray);
            IsToken = isToken;
	        IsOverflow = isOverflow;
        }

        public int PositionInSource { get; }
        public string Text { get; }
        public bool IsToken { get; }
		public bool IsOverflow { get; }
    }
    

    public class TextSlice : TextSliceBase
    {
        internal TextSlice(int positionInSource, char[] charArray, bool isToken, bool isOverFlow) 
            : base(positionInSource, charArray, isToken, isOverFlow)
        { }
    }
    

    public class SearchTextSlice : TextSliceBase
    {
        internal SearchTextSlice(int positionInSource, char[] charArray, bool isToken, bool isOverFlow, SearchTokenMatch match) 
            : base(positionInSource, charArray, isToken, isOverFlow)
        {
            Match = match;
        } 
        
        public SearchTokenMatch Match { get; }
    }
}
