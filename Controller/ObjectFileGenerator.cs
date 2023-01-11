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
        private string ObjectFileView;

        private int FilenameRecordId = 0x80;
        private int AsmVersionRecordId = 0x88;
        private string AssemblerVersion = "Orange Assembler V0.1";

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

        public ResultStatus GenerateObjectFile(string FileName, out List<char> ObjectFile)
        {
            ObjectFile = new List<char>();

            byte ConrtrolByteLenght = 1;
            byte RecordIDByteLenght = 1;
            byte FileNameLenght = (byte)FileName.Length;
            short FirstRecordLenght = (short)(FileNameLenght + ConrtrolByteLenght + RecordIDByteLenght);
            byte FirstRecordLenghtHighByte = (byte)(FirstRecordLenght >> 8);
            byte FirstdRecordLenghtLowByte = (byte)(FirstRecordLenght);

            ObjectFile.Add((char)FilenameRecordId);

            int IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)FirstdRecordLenghtLowByte);
            ObjectFile.Add((char)FirstRecordLenghtHighByte);
            ObjectFile.Add((char)FileNameLenght);
            for (int i = 0; i < FileNameLenght; i++)
            {
                ObjectFile.Add((char)FileName[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            return ResultStatus.OK;
        }

        private char ClalcControlByte(List<char> objectFile, int idPosition)
        {
            byte sum = 0;
            for (int i = idPosition; i < objectFile.Count; i++)
                sum += (byte)objectFile[i];
            sum = (byte)(sum % 256);
            return (char)(256 - sum);
        }
    }
}
