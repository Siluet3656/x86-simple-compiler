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
        public int Value;
        public string Type;

        public SymbolNameInfo(int value, string type)
        {
            this.Type = type;
            this.Value = value;
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

        public ResultStatus TryToGetSymbolNameValue(string SymbolName, out SymbolNameInfo info)
        {
            info = new SymbolNameInfo(0, VarType.NONE);
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
    }
}
