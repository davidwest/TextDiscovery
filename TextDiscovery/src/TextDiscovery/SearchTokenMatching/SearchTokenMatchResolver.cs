

using System;
using System.Collections.Generic;

namespace TextDiscovery
{
    public class SearchTokenMatchResolver : ISearchTokenMatchResolver
    {
        internal enum CharSequenceMatchKind
        {
            Exact,
            StartsWith,
            Contains,
            NoMatch
        }

	    private readonly int _minTokenLength;
	    private readonly int _minStartMatchLength;
	    private readonly bool _ignoreContains;
        private readonly Func<char, char, bool> _equals;
		private readonly Func<char[], bool> _isValidToken;

		public SearchTokenMatchResolver(TokenMatchResolverConfig config)
		{
			_minTokenLength = config.MinTokenLength;
			_minStartMatchLength = config.MinStartMatchLength;
			_ignoreContains = config.IgnoreContains;
			_equals = config.CharEquals;
			_isValidToken = config.IsValidToken;
		}

	    public static SearchTokenMatchResolver Default { get; } = new SearchTokenMatchResolver(new TokenMatchResolverConfig());


	    public SearchTokenMatch TryResolveMatch(char[] token, IReadOnlyList<SearchToken> searchTokens, bool requireExactMatch = false)
        {
	        if (token.Length < _minTokenLength || !_isValidToken(token)) return null;

            SearchTokenMatch match = null;
            var i = 0;
            
			/*-----------------------------------------------------------------
			TODO:
			Could iterate through all and find BEST match instead of FIRST one
			-------------------------------------------------------------------*/
            while (i < searchTokens.Count && match == null)
            {
                match = TryResolveMatch(token, searchTokens[i++], requireExactMatch);
            }

            return match;
        }

        
        private SearchTokenMatch TryResolveMatch(char[] token, SearchToken searchToken, bool requireExactMatch)
        {
            // *** ORDER OF TESTS IS SIGNIFICANT ***

            int start;
            int end;

            var originalMatchKind = TryMatch(token, searchToken.OriginalChars, false, out start, out end);

            var originalStart = start;
            var originalEnd = end;

            // --- Matches original search word exactly? ---
            if (originalMatchKind == CharSequenceMatchKind.Exact)
            {
                return new SearchTokenMatch(token, searchToken, start, end, SearchTokenMatchKind.Exact);
            }

            if (requireExactMatch) return null;

            // --- Is pluralization of search token? ---
            var derivativeMatchKind = TryMatch(token, searchToken.PluralizationChars, true, out start, out end);

            if (derivativeMatchKind == CharSequenceMatchKind.Exact)
            {
                return new SearchTokenMatch(token, searchToken, start, end, SearchTokenMatchKind.DerivativeForm);
            }

            // --- Is singularization of search token? ---
            derivativeMatchKind = TryMatch(token, searchToken.SingularizationChars, true, out start, out end);

            if (derivativeMatchKind == CharSequenceMatchKind.Exact)
            {
                return new SearchTokenMatch(token, searchToken, start, end, SearchTokenMatchKind.DerivativeForm);
            }

            // --- Is lemmatization of search token? ---
            derivativeMatchKind = TryMatch(token, searchToken.LemmatizationChars, true, out start, out end);

            if (derivativeMatchKind == CharSequenceMatchKind.Exact)
            {
                return new SearchTokenMatch(token, searchToken, start, end, SearchTokenMatchKind.DerivativeForm);
            }


            // --- Starts with original search token? ---
            if (originalMatchKind == CharSequenceMatchKind.StartsWith && 
				originalEnd + 1 >= _minStartMatchLength)
            {
                return new SearchTokenMatch(token, searchToken, originalStart, originalEnd, SearchTokenMatchKind.StartsWithOriginal);
            }
			

            // --- Starts with stem of search token? ---
            derivativeMatchKind = TryMatch(token, searchToken.StemChars, true, out start, out end);

            if (derivativeMatchKind == CharSequenceMatchKind.StartsWith &&
				end + 1 >= _minStartMatchLength)
            {
                return new SearchTokenMatch(token, searchToken, start, end, SearchTokenMatchKind.StartsWithStem);
            }

            // --- Contains original search token? ---
            if (!_ignoreContains && 
				originalMatchKind == CharSequenceMatchKind.Contains)
            {
                return new SearchTokenMatch(token, searchToken, originalStart, originalEnd, SearchTokenMatchKind.ContainsOriginal);
            }

            return null;
        }

        private CharSequenceMatchKind TryMatch(char[] token, char[] searchTokenChars, bool requireStartingMatch, out int start, out int end)
        {
            start = -1;
            end = -1;

            if (searchTokenChars == null)
            {
                return CharSequenceMatchKind.NoMatch;
            }

            var tokenLength = token.Length;
            var searchWordLength = searchTokenChars.Length;

            var lastPossibleIndex = tokenLength - searchWordLength;

            var i = 0;

            while (i <= lastPossibleIndex && start < 0)
            {
                var j = 0;

                while (j != searchWordLength &&
                      (i + j) < token.Length &&
                      _equals(token[i + j], searchTokenChars[j]))
                {
                    j++;
                }

                if (j == searchWordLength)
                {
                    start = i;
                    end = i + j - 1;
                }

                if (requireStartingMatch)
                {
                    break;
                }

                i++;
            }

            if (start < 0) return CharSequenceMatchKind.NoMatch;

            var matchKind =
                start == 0
                    ? (end == tokenLength - 1
                        ? CharSequenceMatchKind.Exact
                        : CharSequenceMatchKind.StartsWith)
                    : CharSequenceMatchKind.Contains;

            return matchKind;
        }
    }
}
