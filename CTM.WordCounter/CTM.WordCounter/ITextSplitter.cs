﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTM.WordCountCalculator
{
    public interface ITextSplitter
    {
        IEnumerable<string> SanitizeAndSplitIntoWords(string text);
    }
}
