
using System.Collections.Generic;

namespace TextDiscovery
{
    public abstract class TextSliceGroupBase<TGroupIndicator, TSlice> 
		where TSlice : TextSliceBase
		where TGroupIndicator : struct
    {
        protected TextSliceGroupBase(TGroupIndicator groupIndicator, IReadOnlyList<TSlice> slices)
        {
            GroupIndicator = groupIndicator;
            Slices = slices;
        }
        
        public TGroupIndicator GroupIndicator { get; }
        public IReadOnlyList<TSlice> Slices { get; }
    }
	
    public class TextSliceGroup<TGroupIndicator> : TextSliceGroupBase<TGroupIndicator, TextSlice>
		where TGroupIndicator : struct
    {
        internal TextSliceGroup(TGroupIndicator groupIndicator, IReadOnlyList<TextSlice> slices) 
            : base(groupIndicator, slices)
        { }
    }

    public class SearchTextSliceGroup<TGroupIndicator> : TextSliceGroupBase<TGroupIndicator, SearchTextSlice>
		where TGroupIndicator : struct
    {
        internal SearchTextSliceGroup(TGroupIndicator groupIndicator, IReadOnlyList<SearchTextSlice> slices) 
            : base(groupIndicator, slices)
        { }
    }
}