using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler
{
    internal class OPTAB
    {
        struct Operation
        {
            private string OpName;
            private int OpCode;
            private int OpLength;
            private int AmountOfArgs;

            public Operation(string v1, int v2, int v3, int v4) : this()
            {
                this.OpName = v1;
                this.OpCode = v2;
                this.OpLength = v3;
                this.AmountOfArgs = v4;
            }

            public string GetOpName()
            { return this.OpName; }

            public int GetOpCode()
            { return this.OpCode; }

            public int GetOpLenght()
            { return this.OpLength; }

            public int GetAmountOfArgs()
            {
                return this.AmountOfArgs;
            }
        }

        private static OPTAB single = null;
        private List<Operation> Operations = new List<Operation>();

        protected OPTAB()
        {
            Operations.Add(new Operation("NOP", 0x90, 1, 0));
            Operations.Add(new Operation("JG", 0x7F, 2, 1));
            Operations.Add(new Operation("ADD", 0, 2, 2));
            Operations.Add(new Operation("ROR", 0, 2, 2));
            Operations.Add(new Operation("DEC", 0, 2, 1));
            Operations.Add(new Operation("XOR", 0, 2, 2));
        }

        public static OPTAB Init()
        {
            if (single == null)
            {
                single = new OPTAB();
            }
            return single;
        }

        public ResultStatus GetCodeByName(string Name)
        {
            foreach (Operation op in Operations)
            {
                if (op.GetOpName() == Name)
                {
                    return ResultStatus.OK;
                }
            }
            return ResultStatus.OpDoesNotExist;
        }
        public ResultStatus GetCodeByName(string Name, out int OpCode)
        {
            OpCode = -1;
            foreach (Operation op in Operations)
            {
                if (op.GetOpName() == Name)
                {
                    OpCode = op.GetOpCode();
                    return ResultStatus.OK;
                }

            }
            return ResultStatus.SymbolNameDoesNotExist;
        }
        public ResultStatus GetCodeByName(string Name, out int OpCode, out int OpLenght)
        {
            OpCode = -1;
            OpLenght = -1;
            foreach (Operation op in Operations)
            {
                if (op.GetOpName() == Name)
                {
                    OpCode = op.GetOpCode();
                    OpLenght = op.GetOpLenght();
                    return ResultStatus.OK;
                }

            }
            return ResultStatus.SymbolNameDoesNotExist;
        }

        public int GetOpLength(string operation, string arg1, string arg2)
        {
            foreach (Operation op in Operations)
            {
                if (op.GetOpName() == operation)
                {
                    return op.GetOpLenght();
                }
            }
            return -1;
        }

        public int GetOpLength(string operation, string arg1)
        {
            foreach (Operation op in Operations)
            {
                if (op.GetOpName() == operation)
                {
                    return op.GetOpLenght();
                }
            }
            return -1;
        }

        public int GetOpLength(string operation)
        {
            foreach (Operation op in Operations)
            {
                if (op.GetOpName() == operation)
                {
                    return op.GetOpLenght();
                }
            }
            return -1;
        }

        public int GetOpAmountOfArgs (string operation)
        {
            foreach (Operation op in Operations)
            {
                if (op.GetOpName() == operation)
                {
                    return op.GetAmountOfArgs();
                }
            }
            return -1;
        }
    }
}
