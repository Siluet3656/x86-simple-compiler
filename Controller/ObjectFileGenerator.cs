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
        private string AssemblerVersion = "My Assembler V0.1";

        private int FilenameRecordId = 0x80;
        private int AsmVersionRecordId = 0x88;
        private byte[] AsmVersionConst = { 0x00, 0x00 };
        private int AdditionalNameRecordId = 0x88;
        private byte[] AdditionalRecordConst = { 0x40, 0xE9 };
        private string UndefinedField = "PŠ›U";
        private readonly byte[] CosntRecord = { 0x88, 0x03, 0x00, 0x40, 0xE9, 0x4C, 0x96, 0x02, 0x00, 0x00, 0x68, 0x88, 0x03, 0x00, 0x40, 0xA1, 0x94 };


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

            int IdPosition;
            byte ConrtrolByteLenght = 1;
            byte NameLenghtByteLenght = 1;

            byte FileNameLenght = (byte)FileName.Length;
            short FirstRecordLenght = (short)(FileNameLenght + ConrtrolByteLenght + NameLenghtByteLenght);
            byte FirstRecordLenghtHighByte = GetHightByte(FirstRecordLenght);
            byte FirstdRecordLenghtLowByte = GetLowByte(FirstRecordLenght);

            ObjectFile.Add((char)FilenameRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)FirstdRecordLenghtLowByte);
            ObjectFile.Add((char)FirstRecordLenghtHighByte);
            ObjectFile.Add((char)FileNameLenght);
            for (int i = 0; i < FileNameLenght; i++)
            {
                ObjectFile.Add((char)FileName[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));


            byte AsmVersionLenght = (byte)AssemblerVersion.Length;
            short SecondRecordLenght = (short)(AsmVersionLenght + ConrtrolByteLenght + NameLenghtByteLenght);
            byte SecondRecordLenghtHighByte = GetHightByte(SecondRecordLenght);
            byte SecondRecordLenghtLowByte = GetLowByte(SecondRecordLenght);

            ObjectFile.Add((char)AsmVersionRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)SecondRecordLenghtLowByte);
            ObjectFile.Add((char)SecondRecordLenghtHighByte);
            for (int i = 0; i < AsmVersionConst.Length; i++)
            {
                ObjectFile.Add((char)AsmVersionConst[i]);
            }
            ObjectFile.Add((char)AsmVersionLenght);
            for (int i = 0; i < AsmVersionLenght; i++)
            {
                ObjectFile.Add((char)AssemblerVersion[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            byte UndefinedFieldLenght = 4;
            byte AdditionalRecordConstLenght = 2;
            short ThirdRecordLenght = (short)(FileNameLenght + ConrtrolByteLenght + NameLenghtByteLenght + UndefinedFieldLenght + AdditionalRecordConstLenght);
            byte ThirdRecordLenghtHighByte = GetHightByte(ThirdRecordLenght);
            byte ThirdRecordLenghtLowByte = GetLowByte(ThirdRecordLenght);

            ObjectFile.Add((char)AdditionalNameRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)ThirdRecordLenghtLowByte);
            ObjectFile.Add((char)ThirdRecordLenghtHighByte);
            for (int i = 0; i < AdditionalRecordConst.Length; i++)
            {
                ObjectFile.Add((char)AdditionalRecordConst[i]);
            }
            for (int i = 0; i < UndefinedField.Length; i++)
            {
                ObjectFile.Add((char)UndefinedField[i]);
            }
            ObjectFile.Add((char)FileNameLenght);
            for (int i = 0; i < FileNameLenght; i++)
            {
                ObjectFile.Add((char)FileName[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            for (int i = 0; i < CosntRecord.Length; i++)
            {
                ObjectFile.Add((char)CosntRecord[i]);
            }

            return ResultStatus.OK;
        }

        private byte GetHightByte(short RecordLenght)
        {
            return (byte)(RecordLenght >> 8);
        }
        private byte GetLowByte(short RecordLenght)
        {
            return (byte)RecordLenght;
        }
        private char ClalcControlByte(List<char> objectFile, int idPosition)
        {
            byte sum = 0;
            for (int i = idPosition; i < objectFile.Count; i++)
            {
                sum += (byte)objectFile[i];
            }
            sum = (byte)(sum % 256);
            return (char)(256 - sum);
        }
    }
}
