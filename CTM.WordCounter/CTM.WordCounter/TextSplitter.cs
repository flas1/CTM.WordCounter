using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator
{
    public class TextSplitter : ITextSplitter
    {
        public IEnumerable<string> SanitizeAndSplitIntoWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            var charsToRemove = text.Where(i => !char.IsLetterOrDigit(i) && i != ' ' && i != '\'').Distinct().ToList(); // find non-alphanumeric characters
            foreach (var character in charsToRemove)
            {
                text = text.Replace(character, ' ');
            }

            var words = text.Split(' ');
            return words.Where(w => !string.IsNullOrWhiteSpace(w));
        }
    }
}
