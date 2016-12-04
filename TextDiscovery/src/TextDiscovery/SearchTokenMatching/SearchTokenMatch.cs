
using System;

namespace TextDiscovery
{    
    public enum SearchTokenMatchKind
    {
        ContainsOriginal = 1,
        StartsWithStem = 2,
        StartsWithOriginal = 3,
        DerivativeForm = 4,
        Exact = 5
    }
	
    public class SearchTokenMatch
    {
        internal class TextComponents
        {
            public string PreText { get; set; }
            public string FocalText { get; set; }
            public string PostText { get; set; }
        }
		
        private readonly Lazy<TextComponents> _lazyComponents;
        
        internal SearchTokenMatch(char[] token, SearchToken sourceSearchToken, int start, int end, SearchTokenMatchKind kind)
        {
            SourceSearchToken = sourceSearchToken;
            Token = token;
            StartIndex = start;
            EndIndex = EndIndex;
            Kind = kind;

            _lazyComponents = new Lazy<TextComponents>(() => MakeTextComponents(token, start, end), false);
        }

        private static TextComponents MakeTextComponents(char[] token, int start, int end)
        {
            var preText = new char[start];

            Array.Copy(token, 0, preText, 0, start);

            var focalLength = end - start + 1;
            var focalText = new char[focalLength];

            Array.Copy(token, start, focalText, 0, focalLength);

            var postLength = token.Length - end - 1;
            var postText = new char[postLength];

            Array.Copy(token, end + 1, postText, 0, postLength);

            var components =
                new TextComponents
                {
                    PreText = new string(preText),
                    FocalText = new string(focalText),
                    PostText = new string(postText)
                };

            return components;
        }

        public SearchToken SourceSearchToken { get; }
        public SearchTokenMatchKind Kind { get; }
        public char[] Token { get; }
        public int StartIndex { get; }
        public int EndIndex { get; }

        public string PreText => _lazyComponents.Value.PreText;
        public string FocalText => _lazyComponents.Value.FocalText;
        public string PostText => _lazyComponents.Value.PostText;
        
        public override string ToString()
        {
            return $"{Kind, -20} : [{PreText}, {FocalText}, {PostText}]";
        }
    }
}

