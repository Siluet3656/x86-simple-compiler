using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler.Controller
{
    internal class ObjectFileGenerator
    {
        private static ObjectFileGenerator Single = null;
        private string SourceNameId = "80";
        private string SourceAsmVersionId = "88";
        private Byte[] ObjectFile;
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

        private Byte[] StringToByte(string s)
        {
            Byte[] b;

            b = Encoding.ASCII.GetBytes(s);

            return b;
        }

        public Byte[] GenerateObjectFile()
        {
            ObjectFile = StringToByte(SourceNameId);

            return ObjectFile;
        }
    }
}
