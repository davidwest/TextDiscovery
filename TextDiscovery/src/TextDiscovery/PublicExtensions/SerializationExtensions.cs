
using System;
using System.Collections.Generic;
using System.Linq;

namespace TextDiscovery
{
    public static class SerializationExtensions
    {
	    public static string SerializeToString(this IEnumerable<TextSlice> slices, Func<TextSlice, string> toString, string spacer = "")
	    {
		    return slices.Select(toString).SerializeToString(spacer);
	    }
    }
}
