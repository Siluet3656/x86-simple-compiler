using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    enum KeySymbols
    {
        Dot = '.',
        Comma = ',',
        Space = ' ',
        TwoDots = ':'
    }

    public partial class Form1 : Form
    {
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
                    ProgramText.LoadFile(OpenFileDialog1.FileName, RichTextBoxStreamType.UnicodePlainText);
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
                    ProgramText.SaveFile(saveFileDialog1.FileName, RichTextBoxStreamType.UnicodePlainText);
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
            string Operation, Arg1, Arg2;
            int CurrentAddress = 0;

            const string Main = "MAIN";
            const string End = "END";

            if (ProgramText.Text != string.Empty)
            {
                parser.ParseText(ProgramText.Text, out Lines);
                int[] Address = new int[Lines.Length + 1];
                Address[0] = 0;

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
                                }
                                else if (symtab.TryToGetSymbolNameValue(LineParts[0]) == ResultStatus.OK)
                                {

                                }
                            }
                            else if (optab.GetCodeByName(LineParts[0]) == ResultStatus.OK)
                            {

                            }
                            //for (int j = 0; j < Lines[i].Length; j++)
                            //{

                            //    Operation = LineParts[0];
                            //    Arg1 = LineParts[1];
                            //    Arg2 = LineParts[2];

                            //    Address[j + 1] = optab.GetOpLength(Operation, Arg1, Arg2);

                            //    if (Address[j + 1] > 0)
                            //    {
                            //        CurrentAddress += Address[j + 1];
                            //    }
                            //}
                        }
                    }
                }
            }
        }
    }
}
