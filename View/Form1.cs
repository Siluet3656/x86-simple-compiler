using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using x86_simple_compiler.Controller;

namespace x86_simple_compiler
{
    public partial class Form1 : Form
    {
        private List<char> ObjectFile;
        private bool IsObjectFileGenerated = false;
        private string OnlyFileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ObjectText.BackColor = SystemColors.Window;
        }

        private void ChooseFileBtn_Click(object sender, EventArgs e)
        {
            if (OpenFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ProgramText.Clear();
                    ProgramText.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    OnlyFileName = RemoveFullPathAndGetOnlyName(OpenFileDialog1.FileName);
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Ошибка при открытии.\n\nПричина: {ex.Message}\n\n" +
                    $"Дополнителная информация:\n\n{ex.StackTrace}");
                }
            }
            else
            {
                MessageBox.Show($"Файл {saveFileDialog1.FileName} не удалось открыть!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ProgramText.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.PlainText);
                    MessageBox.Show($"Файл {saveFileDialog1.FileName} успешно сохранён!");
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Ошибка при сохранении.\n\nПричина: {ex.Message}\n\n" +
                    $"Дополнителная информация:\n\n{ex.StackTrace}");
                }

            }
            else
            {
                MessageBox.Show($"Файл {saveFileDialog1.FileName} не удалось сохранить!");
            }
        }

        private void CompileBtn_Click(object sender, EventArgs e)
        {
            Parser parser = Parser.Init();
            OPTAB optab = OPTAB.Init();
            SYMTAB symtab = SYMTAB.Init();
            string[] Lines;
            string[] LineParts;
            string Operation;
            string[] Args;
            string Arg;
            int CurrentCodeAddress = 0;
            int CurrentDataAddress = 0;
            int AmountOfArgs = 0;
            int OpsCounter = 0;

            const string Main = "MAIN";
            const string End = "END";

            if (ProgramText.Text != string.Empty)
            {
                parser.ParseText(ProgramText.Text, out Lines);
                int[] Address = new int[Lines.Length + 1];
                Address[0] = 0;
                int j = 0;

                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Lines[i][0] == (char)KeySymbols.Dot)
                    {

                    }
                    else
                    {
                        LineParts = Lines[i].Split((char)KeySymbols.Space);

                        if (LineParts[0] == Main || LineParts[0] == End)
                        {

                        }
                        else
                        {
                            if (optab.GetCodeByName(LineParts[0]) == ResultStatus.OpDoesNotExist)
                            {
                                if (symtab.TryToGetSymbolNameValue(LineParts[0]) == ResultStatus.SymbolNameDoesNotExist)
                                {
                                    if (LineParts[0][LineParts[0].Length - 1] == (char)KeySymbols.TwoDots)
                                    {
                                        symtab.AddSymbleName(LineParts[0], new SymbolNameInfo(0, "MARK", CurrentCodeAddress));
                                    }
                                    else
                                    {
                                        symtab.AddSymbleName(LineParts[0], new SymbolNameInfo(Int32.Parse(LineParts[2]), LineParts[1], CurrentDataAddress));
                                        CurrentDataAddress++;
                                    }
                                }
                                else if (symtab.TryToGetSymbolNameValue(LineParts[0]) == ResultStatus.OK)
                                {
                                    MessageBox.Show("СИМВОЛЬНЫЕ ИМЕНА ПОВТОРЯЮТСЯ!!!!");
                                }
                            }
                            else if (optab.GetCodeByName(LineParts[0]) == ResultStatus.OK)
                            {
                                Operation = LineParts[0];
                                j++;
                                AmountOfArgs = optab.GetOpAmountOfArgs(LineParts[0]);
                                switch (AmountOfArgs)
                                {
                                    case 0:
                                        Address[j] = optab.GetOpLength(Operation);
                                        break;
                                    case 1:
                                        Arg = LineParts[1];
                                        Address[j] = optab.GetOpLength(Operation, Arg);
                                        break;
                                    case 2:
                                        Args = LineParts[1].Split((char)KeySymbols.Comma);
                                        Address[j] = optab.GetOpLength(Operation, Args[0], Args[1]);
                                        break;
                                }

                                if (Address[j] > 0)
                                {
                                    CurrentCodeAddress += Address[j];
                                    OpsCounter++;
                                }
                            }
                        }
                    }
                }
                /////////////////2nd pass
                int[] OpCodes = new int[OpsCounter];
                j = 0;

                for (int i = 0; i < Lines.Length; i++)
                {
                    if (Lines[i][0] == (char)KeySymbols.Dot)
                    {

                    }
                    else
                    {
                        LineParts = Lines[i].Split((char)KeySymbols.Space);

                        if (LineParts[0] == Main || LineParts[0] == End)
                        {

                        }
                        else
                        {
                            if (optab.GetCodeByName(LineParts[0]) == ResultStatus.OK)
                            {
                                Operation = LineParts[0];
                                if (LineParts.Length > 1)
                                {
                                    Args = LineParts[1].Split((char)KeySymbols.Comma);
                                }
                                else
                                {
                                    Args = new string[3];
                                }
                                int OpLength = 1;
                                switch (LineParts.Length)
                                {
                                    case 1:
                                        optab.GetCodeByName(Operation, out OpCodes[j]);
                                        break;
                                    case 2:
                                        optab.GetCodeByName(Operation, Args, out OpCodes[j]);
                                        string[] Argss = LineParts[1].Split((char)KeySymbols.Comma);

                                        switch (Argss.Length)
                                        {
                                            case 2:
                                                OpLength = optab.GetOpLength(Operation, Argss[0], Argss[1]);
                                                break;
                                            case 1:
                                                OpLength = optab.GetOpLength(Operation, Argss[0]);
                                                break;
                                            default: OpLength = 0; break;
                                        }

                                        break;
                                }


                                switch (OpLength)
                                {
                                    case 1:
                                        break;
                                    case 2:
                                        OpCodes[j] = OpCodes[j] << 8;
                                        break;
                                    case 3:
                                        OpCodes[j] = OpCodes[j] << 16;
                                        break;
                                    case 4:
                                        OpCodes[j] = OpCodes[j] << 24;
                                        break;
                                }
                                int ConstPart = 0, FirstReg = 0, SecondReg = 0, Mod = 0, Mem = 0, disp = 0;

                                switch (Operation)
                                {
                                    case "XOR":
                                        if (Args[0] == "AL")
                                        {
                                            ConstPart = 0b_11 << 6;
                                            FirstReg = Registers.alCode << 3;
                                            SecondReg = Registers.alCode;
                                        }
                                        else if (Args[0] == "BL")
                                        {
                                            ConstPart = 0b_11 << 6;
                                            FirstReg = Registers.blCode << 3;
                                            SecondReg = Registers.blCode;
                                        }
                                        OpCodes[j] = OpCodes[j] + ConstPart + FirstReg + SecondReg;
                                        break;
                                    case "ROR":
                                        if (Int32.Parse(Args[1]) == 1)
                                        {
                                            Mod = 0b_11 << 6;
                                            ConstPart = 0b_001 << 3;
                                            FirstReg = Registers.alCode;
                                            OpCodes[j] = OpCodes[j] + Mod + ConstPart + FirstReg;
                                        }
                                        else if (Int32.Parse(Args[1]) > 1)
                                        {
                                            Mod = 0b_11 << 14;
                                            ConstPart = 0b_001 << 11;
                                            FirstReg = Registers.blCode << 8;
                                            OpCodes[j] = OpCodes[j] + Mod + ConstPart + FirstReg + Int32.Parse(Args[1]);
                                        }
                                        break;
                                    case "ADD":
                                        if (Args[1] == "BL")
                                        {
                                            ConstPart = 0b_11 << 6;
                                            FirstReg = Registers.alCode << 3;
                                            SecondReg = Registers.blCode;
                                            OpCodes[j] = OpCodes[j] + ConstPart + FirstReg + SecondReg;
                                        }
                                        else if (symtab.TryToFindSymbolName(Args[1]) == ResultStatus.OK)
                                        {
                                            SymbolNameInfo info = new SymbolNameInfo();
                                            int buf;
                                            Mod = 0b_00 << 22;
                                            FirstReg = Registers.alCode << 19;
                                            SecondReg = 0b_110 << 16;
                                            if (symtab.TryToGetSymbolNameInfo(Args[1], out info) == ResultStatus.OK)
                                            {
                                                Mem = info.Adr;
                                                buf = Mem << 8;
                                                Mem = Mem >> 8;
                                                Mem = Mem + buf;
                                            }
                                            OpCodes[j] = OpCodes[j] + Mod + FirstReg + SecondReg + Mem;
                                        }
                                        break;
                                    case "DEC":
                                        ConstPart = 0b_11001 << 3;
                                        FirstReg = Registers.blCode;
                                        OpCodes[j] = OpCodes[j] + ConstPart + FirstReg;
                                        break;
                                    case "JG":
                                        SymbolNameInfo infoo;
                                        if (symtab.TryToGetSymbolNameInfo(LineParts[1], out infoo) == ResultStatus.OK)
                                        {
                                            disp = infoo.Adr - (CalculateAdr(Address, 10) + optab.GetOpLength(Operation));
                                        }
                                        byte DsipByte = (byte)disp;
                                        OpCodes[j] = OpCodes[j] + DsipByte;
                                        break;
                                }
                                j++;
                            }
                        }
                    }
                }
                ////Object file genetation
                ObjectFileGenerator ObjGen = ObjectFileGenerator.Init();
                bool IsFirstNotZeroByteArrieved = false;
                int[] DATA = symtab.GetDataSegment();
                int CODELenght = 0;

                for (int i = 0; i < OpCodes.Length; i++)
                {
                    byte[] Bytes = new byte[4];
                    Bytes = DivideINT(OpCodes[i]);
                    IsFirstNotZeroByteArrieved = false;

                    for (j = 0; j < Bytes.Length; j++)
                    {
                        if (Bytes[j] != 0x00)
                        {
                            CODELenght++;
                            IsFirstNotZeroByteArrieved = true;
                        }
                        else
                        {
                            if (IsFirstNotZeroByteArrieved)
                            {
                                CODELenght++;
                            }
                        }
                    }
                }

                int[] CODE = new int[CODELenght];
                int k = 0;

                for (int i = 0; i < OpCodes.Length; i++)
                {
                    byte[] Bytes = new byte[4];
                    Bytes = DivideINT(OpCodes[i]);
                    IsFirstNotZeroByteArrieved = false;

                    for (j = 0; j < Bytes.Length; j++)
                    {
                        if (Bytes[j] != 0x00)
                        {
                            CODE[k] = Bytes[j];
                            k++;
                            IsFirstNotZeroByteArrieved = true;
                        }
                        else
                        {
                            if (IsFirstNotZeroByteArrieved)
                            {
                                CODE[k] = Bytes[j];
                                k++;
                            }
                        }
                    }
                }

                if (ObjGen.GenerateObjectFile(OnlyFileName, DATA, CODE, out ObjectFile) == ResultStatus.OK)
                {
                    IsObjectFileGenerated = true;
                }
                else
                {
                    IsObjectFileGenerated = false;
                }
            }
        }

        private byte[] DivideINT(int v)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)(v >> 24);
            bytes[1] = (byte)(v >> 16);
            bytes[2] = (byte)(v >> 8);
            bytes[3] = (byte)(v);

            return bytes;
        }

        private string RemoveFullPathAndGetOnlyName(string fileName)
        {
            for (int i = fileName.Length - 1; i > 0; i--)
            {
                if (fileName[i] == '\\')
                {
                    fileName = fileName.Remove(0, i + 1);
                    break;
                }
            }
            return fileName;
        }

        private int CalculateAdr(int[] address, int v)
        {
            int Sum = 0;
            for (int i = 0; i <= v; i++)
            {
                Sum += address[i];
            }

            return Sum;
        }

        private void ObjSaveBtn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() == DialogResult.OK)
            {
                if (IsObjectFileGenerated)
                {
                    try
                    {
                        //ObjectText.SaveFile(saveFileDialog2.FileName, RichTextBoxStreamType.PlainText);

                        string path = saveFileDialog2.FileName;
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }

                        using (FileStream fs = File.Create(path))
                        {
                            foreach (char ch in ObjectFile)
                            {
                                fs.WriteByte((byte)ch);
                            }
                        }

                        MessageBox.Show($"Файл {saveFileDialog2.FileName} успешно сохранён!");
                    }
                    catch (SecurityException ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении.\n\nПричина: {ex.Message}\n\n" +
                        $"Дополнителная информация:\n\n{ex.StackTrace}");
                    }

                }
                else
                {
                    MessageBox.Show($"Объектный файл не сгенерирован!");
                }
            }
            else
            {
                MessageBox.Show($"Файл {saveFileDialog2.FileName} не удалось сохранить!");
            }
        }
    }
}