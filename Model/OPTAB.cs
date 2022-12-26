using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler
{
    internal class OPTAB
    {
        enum Registers
        {
            AL,
            BL
        }
        struct Operation
        {
            private string OpName;
            private int OpCode;
            private int OpLength;
            private int AmountOfArgs;

            public const int alCode = 000;
            public const int blCode = 011;

            public Operation(string OpName, int OpCode, int OpLength, int AmountOfArgs) : this()
            {
                this.OpName = OpName;
                this.OpCode = OpCode;
                this.OpLength = OpLength;
                this.AmountOfArgs = AmountOfArgs;
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
            Operations.Add(new Operation("ADD r8 r8", 0x02, 3, 2));
            Operations.Add(new Operation("ADD r8 m8", 0x02, 3, 2));
            Operations.Add(new Operation("ROR r8 1", 0xD0, 2, 2));
            Operations.Add(new Operation("ROR r8 i8", 0xC0, 3, 2));
            Operations.Add(new Operation("DEC", 0xFE, 2, 1));
            Operations.Add(new Operation("XOR r8 r8", 0x32, 2, 2));
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
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == Name)
                {
                    return ResultStatus.OK;
                }
            }
            return ResultStatus.OpDoesNotExist;
        }
        public ResultStatus GetCodeByName(string Name, out int OpCode)
        {
            OpCode = -1;
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == Name)
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
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == Name)
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
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == operation)
                {
                    return op.GetOpLenght();
                }
            }
            return -1;
        }

        public int GetOpLength(string operation, string arg1)
        {
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == operation)
                {
                    return op.GetOpLenght();
                }
            }
            return -1;
        }

        public int GetOpLength(string operation)
        {
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == operation)
                {
                    return op.GetOpLenght();
                }
            }
            return -1;
        }

        public int GetOpAmountOfArgs(string operation)
        {
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == operation)
                {
                    return op.GetAmountOfArgs();
                }
            }
            return -1;
        }
    }
}
