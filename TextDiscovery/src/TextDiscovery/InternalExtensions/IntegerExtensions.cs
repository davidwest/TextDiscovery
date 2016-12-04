
namespace TextDiscovery
{
    internal static class IntegerExtensions
    {
        public static bool IsNumericaAsciiValue(this int value) => value.IsInRange(48, 57);

        public static bool IsAlphaNumericAsciiValue(this int value) => value.IsNumericaAsciiValue() || value.IsAlphaAsciiValue();

        public static bool IsAlphaAsciiValue(this int value) => value.IsInRange(65, 90) || value.IsInRange(97, 122);

        private static bool IsInRange(this int value, int low, int high) => value >= low && value <= high;
    }
}
