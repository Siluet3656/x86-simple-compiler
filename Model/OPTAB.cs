using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace x86_simple_compiler
{
    struct Registers
    {
        public const int alCode = 0b_000;
        public const int blCode = 0b_011;
    }
    internal class OPTAB
    {

        struct Operation
        {
            private string OpName;
            private int OpCode;
            private int OpLength;
            private int AmountOfArgs;

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
            //1-m 2-r 3-i 4-other
            Operations.Add(new Operation("NOP", 0x90, 1, 0));

            Operations.Add(new Operation("JG m8", 0x7F, 2, 1));

            Operations.Add(new Operation("ADD r8 m8", 0x02, 4, 2));
            Operations.Add(new Operation("ADD r8 r8", 0x02, 2, 2));

            Operations.Add(new Operation("ROR r8 i8", 0xC0, 3, 2));
            Operations.Add(new Operation("ROR r8 1", 0xD0, 2, 2));

            Operations.Add(new Operation("DEC r8", 0xFE, 2, 1));

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
            return ResultStatus.OpDoesNotExist;
        }
        public ResultStatus GetCodeByName(string Name, string[] Args, out int OpCode)
        {
            OpCode = -1;
            String[] OpPattern;
            SYMTAB symtab = SYMTAB.Init();

            if (Args.Length == 1)
            {
                foreach (Operation op in Operations)
                {
                    OpPattern = op.GetOpName().Split(' ');
                    if (OpPattern[0] == Name)
                    {
                        if (OpPattern[1] == "m8")
                        {
                            if (symtab.TryToFindSymbolName(Args[0]) == ResultStatus.OK)
                            {
                                OpCode = op.GetOpCode();
                                return ResultStatus.OK;
                            }
                        }
                        else if (OpPattern[1] == "r8")
                        {
                            OpCode = op.GetOpCode();
                            return ResultStatus.OK;
                        }
                    }
                }
            }
            else
            {
                foreach (Operation op in Operations)
                {
                    OpPattern = op.GetOpName().Split(' ');
                    if (OpPattern[0] == Name)
                    {
                        if (OpPattern[1] == "m8")
                        {
                            if (OpPattern[2] == "m8")
                            {
                                if (symtab.TryToFindSymbolName(Args[0]) == ResultStatus.OK)
                                    if (symtab.TryToFindSymbolName(Args[1]) == ResultStatus.OK)
                                    {
                                        OpCode = op.GetOpCode();
                                        return ResultStatus.OK;
                                    }
                            }
                            else if (OpPattern[2] == "r8")
                            {
                                if (symtab.TryToFindSymbolName(Args[0]) == ResultStatus.OK)
                                {
                                    OpCode = op.GetOpCode();
                                    return ResultStatus.OK;
                                }
                            }
                            else if (OpPattern[2] == "i8")
                            {
                                if (symtab.TryToFindSymbolName(Args[0]) == ResultStatus.OK)
                                {
                                    if (Int32.Parse(Args[1]) > 1)
                                    {
                                        OpCode = op.GetOpCode();
                                        return ResultStatus.OK;
                                    }
                                }
                            }
                            else
                            {
                                if (symtab.TryToFindSymbolName(Args[0]) == ResultStatus.OK)
                                {
                                    OpCode = op.GetOpCode();
                                    return ResultStatus.OK;
                                }
                            }
                        }
                        else if (OpPattern[1] == "r8")
                        {
                            if (OpPattern[2] == "m8")
                            {
                                if (symtab.TryToFindSymbolName(Args[1]) == ResultStatus.OK)
                                {
                                    OpCode = op.GetOpCode();
                                    return ResultStatus.OK;
                                }
                            }
                            else if (OpPattern[2] == "r8")
                            {
                                OpCode = op.GetOpCode();
                                return ResultStatus.OK;
                            }
                            else if (OpPattern[2] == "i8")
                            {
                                int OUT;
                                if (Int32.TryParse(Args[1], out OUT))
                                {
                                    if (OUT > 1)
                                    {
                                        OpCode = op.GetOpCode();
                                        return ResultStatus.OK;
                                    }
                                }
                            }
                            else
                            {
                                OpCode = op.GetOpCode();
                                return ResultStatus.OK;
                            }
                        }
                    }
                }
            }

            return ResultStatus.UnknownError;
        }

        public int GetOpLength(string operation, string arg1, string arg2)
        {
            String[] OpPattern;
            foreach (Operation op in Operations)
            {
                OpPattern = op.GetOpName().Split(' ');
                if (OpPattern[0] == operation)
                {
                    if (operation == "ROR")
                    {
                        if (arg2 == OpPattern[2])
                        {
                            return op.GetOpLenght();
                        }
                        else if (Int32.Parse(arg2) > 1)
                        {
                            if (OpPattern[2] == "i8")
                            {
                                return op.GetOpLenght();
                            }
                        }
                    }
                    else if (operation == "ADD")
                    {
                        if (arg2 == "BL")
                        {
                            return 2;
                        }
                        else if (arg2 == "A" || arg2 == "B")
                        {
                            return 4;
                        }
                    }
                    else
                    {
                        return op.GetOpLenght();
                    }
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
