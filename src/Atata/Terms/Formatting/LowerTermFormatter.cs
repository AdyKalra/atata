﻿using System.Linq;

namespace Atata.TermFormatting
{
    public class LowerTermFormatter : ITermFormatter
    {
        public string Format(string[] words)
        {
            return string.Join(" ", words.Select(x => x.ToLower()));
        }
    }
}
