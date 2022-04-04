using Esoterics.InstructionSets;
using Esoterics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class PspspsInstructionSet
    {
        public PspspsInstruction[] Instructions { get; set; }

        public string Name { get; protected set; }

        public string Version { get; protected set; }

        public PspspsInstructionSet(string name, string version, PspspsInstruction[] instructions)
        {
            Instructions = instructions;
            Name = name;
            Version = version;
        }

        public void Execute(byte instruction, int arg, PspspsVM vm)
        {
            Instructions[instruction].Method(arg, vm);
        }

        public string GetKey()
        {
            return $"{Name}-{Version.Split('.')[0]}";
        }

    }
}
