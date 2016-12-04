
using System.Collections;
using System.Collections.Generic;

namespace TextDiscovery
{
    public class SearchToken : IEnumerable<string>
    {
        internal SearchToken(string original, string pluralization, string singularization, string lemmatization, string stem, int index)
        {
            IndexInList = index;

            Original = original.ToLower();
            Pluralization = pluralization?.ToLower();
            Singularization = singularization?.ToLower();
            Lemmatization = lemmatization?.ToLower();
            Stem = stem?.ToLower();

            OriginalChars = Original.ToCharArray();
            PluralizationChars = Pluralization?.ToCharArray();
            SingularizationChars = Singularization?.ToCharArray();
            LemmatizationChars = Lemmatization?.ToCharArray();
            StemChars = Stem?.ToCharArray();
        }
        
        public int IndexInList { get; }

        public string Original { get; }
        public string Pluralization { get; }
        public string Singularization { get; }
        public string Lemmatization { get; }
        public string Stem { get; }

        internal char[] OriginalChars { get; }
        internal char[] PluralizationChars { get; }
        internal char[] SingularizationChars { get; }
        internal char[] LemmatizationChars { get; }
        internal char[] StemChars { get; }


        public IEnumerator<string> GetEnumerator()
        {
            yield return Original;

            if (Pluralization != null) yield return Pluralization;

            if (Singularization != null) yield return Singularization;

            if (Lemmatization != null) yield return Lemmatization;

            if (Stem != null) yield return Stem;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            var other = obj as SearchToken;

            return other != null && Equals(other);
        }

        protected bool Equals(SearchToken other)
        {
            return string.Equals(Original, other.Original);
        }

        public override int GetHashCode()
        {
            return Original.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Original}{(Pluralization != null ? $" [plr: {Pluralization}]" : "")}{(Singularization != null ? $" [sng: {Singularization}]" : "")}{(Lemmatization != null ? $" [lemma: {Lemmatization}]" : "")}{(Stem != null ? $" [stem: {Stem}]" : "")}";
        }
    }
}
