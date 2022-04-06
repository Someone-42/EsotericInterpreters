using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class PspspsCode
    {

        public string InstructionSetKey;

        public readonly byte[] Instructions;
        public readonly int[] Arguments;

        public PspspsCode(byte[] code, int[] args, string instrKey)
        {
            Instructions = (byte[]) code.Clone();
            Arguments = (int[]) args.Clone();
            InstructionSetKey = instrKey;
        }

    }
}
