using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator
{
    public class WordCounter : IWordCounter
    {
        public IDictionary<string, int> CountWords(IEnumerable<string> words)
        {
            if (words != null)
            {
                return words.GroupBy(w => w.ToLowerInvariant()).ToDictionary(g => g.Key, g => g.Count());
            }
            else
            {
                return new Dictionary<string, int>();
            }
        }
    }
}
