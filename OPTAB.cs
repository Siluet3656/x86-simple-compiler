using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler
{
    internal class OPTAB
    {
        private static OPTAB single = null;

        protected OPTAB() { }

        public static OPTAB Init()
        {
            if (single == null)
            {
                single = new OPTAB();
            }
            return single;
        }
    }
}
