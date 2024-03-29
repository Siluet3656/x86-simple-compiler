﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace x86_simple_compiler
{
    enum ResultStatus
    {
        OK,
        SymbolNameAlreadyExist,
        SymbolNameDoesNotExist,
        SymbolNameIsTooLong,
        OpDoesNotExist,
        UnknownError
    }
    enum KeySymbols
    {
        Dot = '.',
        Comma = ',',
        Space = ' ',
        TwoDots = ':'
    }

    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
