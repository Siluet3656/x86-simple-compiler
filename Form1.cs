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

namespace x86_simple_compiler
{
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
                    ProgramText.LoadFile(OpenFileDialog1.FileName);
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
                    ProgramText.SaveFile(saveFileDialog1.FileName);
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
    }
}
