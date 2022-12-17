using System.Collections.Generic;

namespace x86_simple_compiler
{
    enum ResultStatus
    {
        OK,
        SymbolNameAlreadyExist,
        SymbolNameDoesNotExist,
        SymbolNameIsTooLong,
        UnknownError
    }
    internal class SYMTAB
    {
        private static SYMTAB single = null;
        private Dictionary<string, int> Table = new Dictionary<string, int>();
        private const int MaxLenght = 32;

        protected SYMTAB() { }

        public static SYMTAB Init()
        {
            if (single == null)
            {
                single = new SYMTAB();
            }
            return single;
        }

        public ResultStatus AddSymbleName(string SymbolName, int Value)
        {
            if (SymbolName.Length > MaxLenght)
            {
                return ResultStatus.SymbolNameIsTooLong;
            }

            if (Table.ContainsKey(SymbolName))
            {
                return ResultStatus.SymbolNameAlreadyExist;
            }

            Table.Add(SymbolName, Value);

            if (Table.ContainsKey(SymbolName))
            {
                return ResultStatus.OK;
            }

            return ResultStatus.UnknownError;
        }

        public ResultStatus TryToGetSymbolNameValue(string SymbolName, out int Value)
        {
            Value = 0;
            if (SymbolName.Length > MaxLenght)
            {
                return ResultStatus.SymbolNameIsTooLong;
            }

            if (!Table.ContainsKey(SymbolName))
            {
                return ResultStatus.SymbolNameDoesNotExist;
            }

            Table.TryGetValue(SymbolName, out Value);

            if (Table.ContainsKey(SymbolName))
            {
                return ResultStatus.OK;
            }

            return ResultStatus.UnknownError;
        }
    }
}
