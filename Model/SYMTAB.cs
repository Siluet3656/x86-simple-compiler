using System;
using System.Collections.Generic;

namespace x86_simple_compiler
{
    struct VarType
    {
        public const string NONE = "";
        public const string BYTE = "BYTE";
        public const string WORD = "WORD";
        public const string DWORD = "DWORD";
    }
    struct SymbolNameInfo
    {
        public int Adr;
        public int Value;
        public string Type;

        public SymbolNameInfo(int value, string type, int adr)
        {
            this.Type = type;
            this.Value = value;
            this.Adr = adr;
        }
    }
    internal class SYMTAB
    {
        private static SYMTAB single = null;
        private Dictionary<string, SymbolNameInfo> Table = new Dictionary<string, SymbolNameInfo>();
        private const int MaxLenght = 32;

        public static SYMTAB Init()
        {
            if (single == null)
            {
                single = new SYMTAB();
            }
            return single;
        }

        public ResultStatus AddSymbleName(string SymbolName, SymbolNameInfo Info)
        {
            if (SymbolName.Length > MaxLenght)
            {
                return ResultStatus.SymbolNameIsTooLong;
            }

            switch (Info.Type)
            {
                case "DB":
                    Info.Type = "BYTE";
                    break;
                case "DW":
                    Info.Type = "WORD";
                    break;
                case "DD":
                    Info.Type = "DWORD";
                    break;
                case "MARK":
                    SymbolName = SymbolName.Remove(SymbolName.Length - 1,1);
                    break;
            }

            if (Table.ContainsKey(SymbolName))
            {
                return ResultStatus.SymbolNameAlreadyExist;
            }

            Table.Add(SymbolName, Info);

            if (Table.ContainsKey(SymbolName))
            {
                return ResultStatus.OK;
            }

            return ResultStatus.UnknownError;
        }

        public ResultStatus TryToGetSymbolNameValue(string SymbolName)
        {
            SymbolNameInfo Info;
            if (SymbolName.Length > MaxLenght)
            {
                return ResultStatus.SymbolNameIsTooLong;
            }

            if (!Table.ContainsKey(SymbolName))
            {
                return ResultStatus.SymbolNameDoesNotExist;
            }

            Table.TryGetValue(SymbolName, out Info);

            if (Table.ContainsKey(SymbolName))
            {
                return ResultStatus.OK;
            }

            return ResultStatus.UnknownError;
        }

        public ResultStatus TryToGetSymbolNameInfo(string SymbolName, out SymbolNameInfo info)
        {
            info = new SymbolNameInfo(0, VarType.NONE, 0);
            if (SymbolName.Length > MaxLenght)
            {
                return ResultStatus.SymbolNameIsTooLong;
            }

            if (!Table.ContainsKey(SymbolName))
            {
                return ResultStatus.SymbolNameDoesNotExist;
            }

            Table.TryGetValue(SymbolName, out info);

            if (Table.ContainsKey(SymbolName))
            {
                return ResultStatus.OK;
            }

            return ResultStatus.UnknownError;
        }

        public ResultStatus TryToFindSymbolName(string SymbolName)
        {
            if (SymbolName.Length > MaxLenght)
            {
                return ResultStatus.SymbolNameIsTooLong;
            }

            if (SymbolName != null)
            {
                foreach (String sn in Table.Keys)
                {
                    if (sn == SymbolName)
                    {
                        return ResultStatus.OK;
                    }
                }
            }
            return ResultStatus.SymbolNameDoesNotExist;
        }
    }
}
