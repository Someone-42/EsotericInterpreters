using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class PspspsCode
    {

        public readonly byte[] instructions;
        public readonly int[] args;

        public PspspsCode(byte[] code, int[] args)
        {
            instructions = (byte[]) code.Clone();
            this.args = (int[]) args.Clone();
        }

    }
}
