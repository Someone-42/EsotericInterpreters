using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenSharp.Exceptions;
using ChickenSharp.Interfaces;

namespace ChickenSharp.Interpreter
{
    public class InstructionSet : IInstructionSet
    {
        public Instruction[] Instructions { get; set; }

        public string Name { get; protected set; }

        public string Version { get; protected set; }

        public InstructionSet(Instruction[] instructions, string name, string version)
        {
            Instructions = instructions;
            Name = name;
            Version = version;
        }

        public void Execute(int instruction, int arg, IVM vm)
        {
            if (instruction >= Instructions.Length) 
                throw new ChickenException($"The instruction {instruction} is not compatible with the current InstructionSet");

            Instructions[instruction].Method(arg, vm);

        }
    }
}
