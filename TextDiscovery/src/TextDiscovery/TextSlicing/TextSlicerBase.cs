
using System;
using System.Collections.Generic;

namespace TextDiscovery
{    
    public abstract class TextSlicerBase<TSlice> where TSlice : TextSliceBase
    {
        private readonly Func<char, bool> _isPartOfToken;
        
        protected TextSlicerBase(TextSlicerConfig config)
        {
            _isPartOfToken = config.IsPartOfToken;
        }

        protected IEnumerable<TSlice> CreateSlices(char[] source, int maxTokenCount, Func<int, char[], bool, bool, TSlice> createSlices)
        {
            var i = 0;
            var tokenCount = 0;

            while (i < source.Length && tokenCount <= maxTokenCount)
            {
                var currentSliceIsToken = _isPartOfToken(source[i]);

                tokenCount += currentSliceIsToken ? 1 : 0;

                var characterIsPartOfSlice =
                    currentSliceIsToken
                        ? _isPartOfToken
                        : (c => !_isPartOfToken(c));

                var j = 0;
                while ( (i + j) < source.Length && 
                        characterIsPartOfSlice(source[i + j]))
                {
                    j ++;
                }
                
                var charactersInThisSlice = new char[j]; 
                Array.Copy(source, i, charactersInThisSlice, 0, j);
				
                yield return createSlices(i, charactersInThisSlice, currentSliceIsToken, tokenCount > maxTokenCount);

                i += j;
            }
        }
    }
}
