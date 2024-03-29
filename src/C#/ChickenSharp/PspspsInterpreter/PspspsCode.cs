﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class PspspsCode
    {

        public string InstructionSetKey;
        public string InstructionSetVersion;

        public int ListedMemorySize;
        public int ListedFunctionStackSize;

        public readonly byte[] Instructions;
        public readonly int[] Arguments;
        public readonly int[] LabelAddresses;

        public PspspsCode(byte[] code, int[] args, int[] labelAddresses, string instrKey, string instrSetVersion)
        {
            Instructions = (byte[]) code.Clone();
            Arguments = (int[]) args.Clone();
            LabelAddresses = (int[]) labelAddresses.Clone();
            InstructionSetKey = instrKey;
            InstructionSetVersion = instrSetVersion;
        }

    }
}
