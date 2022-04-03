using Esoterics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class PspspsVM
    {

        public IInstructionSet InstructionSet;

        public FixedStack<int> Memory;

        /// <summary>
        /// Creates a new VM that can execute Pspsps Code
        /// </summary>
        /// <param name="MemSize4">The memory capacity (Stack of Integers, so 4 bytes per added capacity)</param>
        public PspspsVM(IInstructionSet set, int MemSize4 = 1024)
        {
            Memory = new FixedStack<int>(MemSize4);
        }

        public void Execute(PspspsCode code)
        {

        }

    }
}
