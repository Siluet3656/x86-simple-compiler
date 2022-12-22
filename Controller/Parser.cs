using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler.Controller
{
    internal class Parser
    {
        enum CharSet
        {
            LatinSmallMin = 97,
            LatinSmallMax = 122,
            LatinSmallBigDiff = 32
        }

        private static Parser single = null;

        protected Parser() { }

        public static Parser Init()
        {
            if (single == null)
            {
                single = new Parser();
            }
            return single;
        }
        public void ParseText(string Text, out string[] Lines)
        {
            for (int i = 0; i < Text.Length; i++)
            {
                if (Text[i] >= (char)CharSet.LatinSmallMin && Text[i] <= (char)CharSet.LatinSmallMax)
                {
                    StringBuilder TextSB = new StringBuilder(Text);
                    TextSB[i] = (char)(Text[i] - (char)CharSet.LatinSmallBigDiff);
                    Text = TextSB.ToString();
                }
            }

            Lines = Text.Split('\n');
            return;
        }
    }

}
