using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator
{
    public interface IWordCounter
    {
        IDictionary<string, int> CountWords(IEnumerable<string> words);
    }
}
