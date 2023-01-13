using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler.Controller
{
    enum SegmentType
    {
        BYTEpublic = 0x28,
        WORDpublic = 0x48
    }
    internal class ObjectFileGenerator
    {
        private static ObjectFileGenerator Single = null;
        private string ObjectFileView;
        private readonly string AssemblerVersion = "My Assembler V0.1";

        private readonly int FilenameRecordId = 0x80;
        private readonly int AsmVersionRecordId = 0x88;
        private readonly byte[] AsmVersionConst = { 0x00, 0x00 };
        private readonly int AdditionalNameRecordId = 0x88;
        private readonly byte[] AdditionalRecordConst = { 0x40, 0xE9 };
        private readonly string UndefinedField = "PŠ›U";
        private readonly byte[] CosntRecord = { 0x88, 0x03, 0x00, 0x40, 0xE9, 0x4C, 0x96, 0x02, 0x00, 0x00, 0x68, 0x88, 0x03, 0x00, 0x40, 0xA1, 0x94 };
        private readonly string _TextSegment = "_TEXT";
        private readonly string _DataSegment = "_DATA";
        private readonly string DataSegment = "DATA";
        private readonly string CodeSegment = "CODE";
        private readonly string Dgroup = "DGROUP";
        private readonly int SegmentNameDescriptionRecordId = 0x96;
        private readonly int SegmentDataDescriptionRecordId = 0x98;
        private readonly int DGROUPSegmentDataDescriptionRecordId = 0x9A;

        private readonly byte[] ObjectFileEnd = { 0x8A, 0x02, 0x00, 0x00, 0x74 };

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
            byte ControlByteLenght = 1;
            byte NameLenghtByteLenght = 1;
            //1
            byte FileNameLenght = (byte)FileName.Length;
            short FirstRecordLenght = (short)(FileNameLenght + ControlByteLenght + NameLenghtByteLenght);
            byte FirstRecordLenghtHighByte = GetHightByte(FirstRecordLenght);
            byte FirstRecordLenghtLowByte = GetLowByte(FirstRecordLenght);

            ObjectFile.Add((char)FilenameRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)FirstRecordLenghtLowByte);
            ObjectFile.Add((char)FirstRecordLenghtHighByte);
            ObjectFile.Add((char)FileNameLenght);
            for (int i = 0; i < FileNameLenght; i++)
            {
                ObjectFile.Add((char)FileName[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));


            byte AsmVersionLenght = (byte)AssemblerVersion.Length;
            short SecondRecordLenght = (short)(AsmVersionLenght + ControlByteLenght + NameLenghtByteLenght);
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
            short ThirdRecordLenght = (short)(FileNameLenght + ControlByteLenght + NameLenghtByteLenght + UndefinedFieldLenght + AdditionalRecordConstLenght);
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
            //2
            FirstRecordLenght = (short)(_TextSegment.Length + CodeSegment.Length + ControlByteLenght + NameLenghtByteLenght * 2);
            FirstRecordLenghtHighByte = GetHightByte(FirstRecordLenght);
            FirstRecordLenghtLowByte = GetLowByte(FirstRecordLenght);

            ObjectFile.Add((char)SegmentNameDescriptionRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)FirstRecordLenghtLowByte);
            ObjectFile.Add((char)FirstRecordLenghtHighByte);
            ObjectFile.Add((char)_TextSegment.Length);
            for (int i = 0; i < _TextSegment.Length; i++)
            {
                ObjectFile.Add((char)_TextSegment[i]);
            }
            ObjectFile.Add((char)CodeSegment.Length);
            for (int i = 0; i < CodeSegment.Length; i++)
            {
                ObjectFile.Add((char)CodeSegment[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            byte SegmentLenghtLenght = 2;
            byte SegmentNumberLenght = 3;
            byte SegmentTypeLenght = 1;
            short RecordLenght = (short)(SegmentLenghtLenght + SegmentNumberLenght + SegmentTypeLenght + ControlByteLenght);
            byte RecordLenghtHighByte = GetHightByte(RecordLenght);
            byte RecordLenghtLowByte = GetLowByte(RecordLenght);
            byte[] SegmentLenght = { 0x1D, 0x00 };
            byte[] SegmentNumber = { 0x02, 0x03, 0x01 };

            ObjectFile.Add((char)SegmentDataDescriptionRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)RecordLenghtLowByte);
            ObjectFile.Add((char)RecordLenghtHighByte);
            ObjectFile.Add((char)SegmentType.WORDpublic);
            for (int i = 0; i < SegmentLenght.Length; i++)
            {
                ObjectFile.Add((char)SegmentLenght[i]);
            }
            for (int i = 0; i < SegmentNumber.Length; i++)
            {
                ObjectFile.Add((char)SegmentNumber[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            byte DataLenght = (byte)DataSegment.Length;
            byte _DataLenght = (byte)_DataSegment.Length;
            byte _DataLenghtLenght = 1;
            byte DataLenghtLenght = 1;
            RecordLenght = (short)(DataLenght + _DataLenght + _DataLenghtLenght + DataLenghtLenght + ControlByteLenght);
            RecordLenghtHighByte = GetHightByte(RecordLenght);
            RecordLenghtLowByte = GetLowByte(RecordLenght);

            ObjectFile.Add((char)SegmentNameDescriptionRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)RecordLenghtLowByte);
            ObjectFile.Add((char)RecordLenghtHighByte);
            ObjectFile.Add((char)_DataLenght);
            for (int i = 0; i < _DataSegment.Length; i++)
            {
                ObjectFile.Add((char)_DataSegment[i]);
            }
            ObjectFile.Add((char)DataLenght);
            for (int i = 0; i < DataSegment.Length; i++)
            {
                ObjectFile.Add((char)DataSegment[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            SegmentTypeLenght = 1;
            SegmentLenghtLenght = 2;
            SegmentNumberLenght = 3;
            RecordLenght = (short)(SegmentTypeLenght + SegmentLenghtLenght + SegmentNumberLenght + ControlByteLenght);
            RecordLenghtHighByte = GetHightByte(RecordLenght);
            RecordLenghtLowByte = GetLowByte(RecordLenght);
            SegmentLenght = new byte[] { 0x02, 0x00 };
            SegmentNumber = new byte[] { 0x04, 0x05, 0x01 };

            ObjectFile.Add((char)SegmentDataDescriptionRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)RecordLenghtLowByte);
            ObjectFile.Add((char)RecordLenghtHighByte);
            ObjectFile.Add((char)SegmentType.WORDpublic);
            for (int i = 0; i < SegmentLenght.Length; i++)
            {
                ObjectFile.Add((char)SegmentLenght[i]);
            }
            for (int i = 0; i < SegmentNumber.Length; i++)
            {
                ObjectFile.Add((char)SegmentNumber[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            byte SegmentNameLenghtLenght = 1;
            byte SegmentNameLenght = (byte)Dgroup.Length;
            RecordLenght = (short)(SegmentNameLenghtLenght + SegmentNameLenght + ControlByteLenght);
            RecordLenghtHighByte = GetHightByte(RecordLenght);
            RecordLenghtLowByte = GetLowByte(RecordLenght);

            ObjectFile.Add((char)SegmentNameDescriptionRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)RecordLenghtLowByte);
            ObjectFile.Add((char)RecordLenghtHighByte);
            ObjectFile.Add((char)SegmentNameLenght);
            for (int i = 0; i < SegmentNameLenght; i++)
            {
                ObjectFile.Add((char)Dgroup[i]);
            }
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));

            byte DGROUPconst = 0xFF;
            byte SerialNumber = 0x06;
            byte NumberOfRegisterInServiceGroup = 0x02;
            byte SerialNumberByteLenght = 1;
            byte ConstLenght = 1;
            byte NumberOfRegisterInServiceGroupByteLenght = 1;
            RecordLenght = (short)(SerialNumberByteLenght + ConstLenght + NumberOfRegisterInServiceGroupByteLenght + ControlByteLenght);
            RecordLenghtHighByte = GetHightByte(RecordLenght);
            RecordLenghtLowByte = GetLowByte(RecordLenght);

            ObjectFile.Add((char)DGROUPSegmentDataDescriptionRecordId);
            IdPosition = ObjectFile.Count - 1;

            ObjectFile.Add((char)RecordLenghtLowByte);
            ObjectFile.Add((char)RecordLenghtHighByte);
            ObjectFile.Add((char)SerialNumber);
            ObjectFile.Add((char)DGROUPconst);
            ObjectFile.Add((char)NumberOfRegisterInServiceGroup);
            ObjectFile.Add((char)ClalcControlByte(ObjectFile, IdPosition));
            //3

            //4
            for (int i = 0; i < ObjectFileEnd.Length; i++)
            {
                ObjectFile.Add((char)ObjectFileEnd[i]);
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
