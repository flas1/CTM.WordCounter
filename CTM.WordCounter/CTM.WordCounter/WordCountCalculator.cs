using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator
{
    public class WordCountCalculator : IWordCountCalculator
    {
        private readonly ITextSplitter textSplitter;
        private readonly IWordCounter wordCounter;
        private readonly IPrimeNumberChecker primeChecker;

        private readonly Dictionary<int, bool> primeCheckResults = new Dictionary<int, bool>();
        private readonly List<CountedWord> countedWords = new List<CountedWord>();

        public WordCountCalculator(ITextSplitter textSplitter, IWordCounter wordCounter, IPrimeNumberChecker primeChecker)
        {
            this.textSplitter = textSplitter;
            this.wordCounter = wordCounter;
            this.primeChecker = primeChecker;
            
        }

        public IEnumerable<CountedWord> CountedWords
        {
            get
            {
                return countedWords;
            }
        }

        public void CalculateAndUpdateWordCount(string text)
        {
            var words = textSplitter.SanitizeAndSplitIntoWords(text);
            var wordCount = wordCounter.CountWords(words);
            foreach (var kvp in wordCount)
            {
                var existingWord = countedWords.SingleOrDefault(cw => cw.Word.Equals(kvp.Key));
                if (existingWord == null)
                {
                    existingWord = new CountedWord { Word = kvp.Key, Count = kvp.Value };
                    countedWords.Add(existingWord);
                }
                else
                {
                    existingWord.Count += kvp.Value;
                }

                bool isPrime;
                if (!primeCheckResults.TryGetValue(existingWord.Count, out isPrime))
                {
                    isPrime = primeChecker.IsPrime(existingWord.Count);
                    primeCheckResults[existingWord.Count] = isPrime;
                }

                existingWord.IsPrime = isPrime;
            }
        }
    }
}
