using Esoterics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class PspspsVM
    {

        public PspspsInstructionSet InstructionSet;
        public FixedStack<int> Memory;

        #region Runtime
        public int InstructionPointer;
        public PspspsCode Code;
        #endregion Runtime

        /// <summary>
        /// Creates a new VM that can execute Pspsps Code
        /// </summary>
        /// <param name="MemSize4">The memory capacity (Stack of Integers, so 4 bytes per added capacity)</param>
        public PspspsVM(PspspsInstructionSet set, int MemSize4 = 1024)
        {
            Memory = new FixedStack<int>(MemSize4);
            InstructionSet = set;
        }

        public void Execute(PspspsCode code)
        {
            if (code.InstructionSetKey != InstructionSet.GetKey())
                throw new Exception($"Incompatible sets. The code is from the {code.InstructionSetKey} instruction set, whilst the VM is running {InstructionSet.GetKey()}");

            Code = code;                // Initializing
            InstructionPointer = 0;

            while (InstructionPointer < code.Instructions.Length)
            {
                //Debug();
                InstructionSet.Execute(code.Instructions[InstructionPointer], code.Arguments[InstructionPointer], this);
                InstructionPointer++;
            }

            InstructionPointer = -1;    // Flushing memory
            Code = null;

        }

        private void Debug()
        {
            int instruction = Code.Instructions[InstructionPointer];
            int arg = Code.Arguments[InstructionPointer];
            List<string> memString = new List<string>();
            foreach (int i in Memory)
                memString.Add(i.ToString());
            Console.WriteLine($"STEP IP={InstructionPointer} : {InstructionSet.Instructions[instruction].Name}, {arg} | {string.Join(", ", memString)}");

        }

    }
}
