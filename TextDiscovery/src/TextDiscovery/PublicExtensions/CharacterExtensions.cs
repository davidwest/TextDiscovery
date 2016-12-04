
namespace TextDiscovery
{
    public static class CharacterExtensions
    {
        public static bool IsNumeric(this char c)
        {
            var value = (int)c;

            return value.IsNumericaAsciiValue();
        }

        public static bool IsAlpha(this char c)
        {
            var value = (int)c;
            return value.IsAlphaAsciiValue();
        }

        public static bool IsAlphaNumeric(this char c)
        {
            var value = (int)c;

            return value.IsAlphaNumericAsciiValue();
        }

        public static bool IsAlphaNumericOrApostrophe(this char c)
        {
            return c.IsAlphaNumeric() || c == '\'' || c == '’';
        }

        public static bool CaseInsensitiveEquals(this char c1, char c2)
        {
            return char.ToLowerInvariant(c1) == char.ToLowerInvariant(c2);
        }
    }
}
