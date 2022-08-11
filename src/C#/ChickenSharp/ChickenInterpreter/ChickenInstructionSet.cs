using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Esoterics.Exceptions;
using Esoterics.InstructionSets;
using Esoterics.ChickenInterfaces;

namespace Esoterics.ChickenInterpreter
{
    public class ChickenInstructionSet : IInstructionSet
    {
        public Instruction[] Instructions { get; set; }

        public string Name { get; protected set; }

        public string Version { get; protected set; }

        public ChickenInstructionSet(string name, string version, Instruction[] instructions)
        {
            Instructions = instructions;
            Name = name;
            Version = version;
        }

        public void Execute(int instruction, int? arg, IVM vm)
        {
            if (instruction >= Instructions.Length) 
                throw new ChickenException($"The instruction {instruction} is not compatible with the current InstructionSet");

            Instructions[instruction].Method(arg, vm);

        }

        public string GetKey()
        {
            return $"{Name}-{Version.Split('.')[0]}";
        }

    }
}
