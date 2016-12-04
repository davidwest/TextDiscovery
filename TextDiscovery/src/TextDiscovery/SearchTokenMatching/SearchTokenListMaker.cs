
using System;
using System.Collections.Generic;
using System.Linq;

namespace TextDiscovery
{    
    public class SearchTokenListMaker : ISearchTokenListMaker
    {
	    private readonly ITextSlicer _slicer;
	    private readonly int _minTokenLength;
        private readonly Func<string, bool> _isValidToken; 
		
        public SearchTokenListMaker(ITextSlicer slicer, TokenListMakerConfig config)
        {
	        _slicer = slicer;
	        _minTokenLength = config.MinTokenLength;
            _isValidToken = config.IsValidToken;
        }

	    public static SearchTokenListMaker Default { get; } = new SearchTokenListMaker(TextSlicer.Default, new TokenListMakerConfig());


	    public IReadOnlyList<SearchToken> MakeTokensFrom(string searchExpression)
        {
            var originalTokens = 
                _slicer.CreateSlices(searchExpression)
                .Where(slice => slice.IsToken && 
					   slice.Text.Length >= _minTokenLength && 
					   _isValidToken(slice.Text))
                .Select(slice => slice.Text.ToLower())
                .Distinct()
                .ToArray();

            var searchTokens = new List<SearchToken>();
            var masterTokenList = new List<string>();
            
            Func<string, string> tryRegister = token =>
            {
                if (string.IsNullOrWhiteSpace(token) || masterTokenList.Contains(token)) return null;

                masterTokenList.Add(token);
                return token;
            };

            for (var i = 0; i != originalTokens.Length; i ++)
            {
                var token = originalTokens[i];

                masterTokenList.Add(token);

                var pluralization = tryRegister(Pluralize(token));
                var singularization = tryRegister(Singularize(token));
                var lemmatization = tryRegister(Lemmatize(token));
                var stem = tryRegister(Stem(token));

                searchTokens.Add(new SearchToken(token, pluralization, singularization, lemmatization, stem, i));
            }

            return searchTokens;
        }

        protected virtual string Pluralize(string source) {return null;}
        protected virtual string Singularize(string source) { return null;}
        protected virtual string Lemmatize(string source) { return null;}
        protected virtual string Stem(string source) { return null;}

    }
}
