
using System;
using Humanizer;
using SF.Snowball.Ext;

namespace TextDiscovery.DemoConsole
{
    public class SpecializedSearchTokenListMaker : SearchTokenListMaker
    {
        public SpecializedSearchTokenListMaker(ITextSlicer slicer, TokenListMakerConfig config)
            : base(slicer, config)
        { }

        protected override string Stem(string source)
        {
            source = RemoveApostrophes(source);

            var stemmer = new EnglishStemmer();
            stemmer.SetCurrent(source);
            stemmer.Stem();
            return stemmer.GetCurrent();
        }

        protected override string Lemmatize(string source)
        {
	        source = RemoveApostrophes(source);

	        return source.Equals("email", StringComparison.OrdinalIgnoreCase) ? "mail" : source;
        }

        protected override string Pluralize(string source)
        {
            return RemoveApostrophes(source)?.Pluralize();
        }

        protected override string Singularize(string source)
        {
            return RemoveApostrophes(source)?.Singularize();
        }

        private static string RemoveApostrophes(string source)
        {
            var parts = source.Split('\'');
            return parts.Length == 0 ? null : parts[0];
        }
    }
}
