using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler.Controller
{
    internal class ObjectFileGenerator
    {
        private static ObjectFileGenerator Single = null;
        private int SourceNameId = 0x80;
        private int SourceAsmVersionId = 0x88;
        private string ObjectFileView;

        protected ObjectFileGenerator()
        {
            
        }

        public static ObjectFileGenerator Init()
        {
            if (Single == null)
            {
                Single = new ObjectFileGenerator();
            }

            return Single;
        }

        public ResultStatus GenerateObjectFile(out List<char> ObjectFile)
        {
            ObjectFile = new List<char>();

            ObjectFile.Add((char)SourceNameId);

            return ResultStatus.OK;
        }
    }
}
