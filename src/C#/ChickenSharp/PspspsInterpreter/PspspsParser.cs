using Esoterics.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Esoterics.InstructionSets;

namespace Esoterics.PspspsInterpreter
{
    public static class PspspsParser
    {

        public static PspspsCode CodeFromString(string code, IInstructionSet instructionSet)
        {
            string[] sInstructions = code.Split(new char[]{ '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<byte> instructions = new List<byte>(sInstructions.Length);         // Setting the capacity to the amount of lines, which will pretty much always be enough
            List<int> arguments = new List<int>(sInstructions.Length);

            List<Instruction> set = instructionSet.Instructions.ToList();

            for(int i = 0; i < sInstructions.Length; i++)
            {
                string sInstruction = sInstructions[i];
                int ins = -1, arg = 0;

                //TODO: Remove comments (//)
                //TODO: Skip comment only lines or Empty lines
                //TODO: Remove /*multiple line*/ comments

                string[] sInAr = sInstruction.ToLowerInvariant().Trim().Split(' '); // Returns an array, with the instruction to decode, and the argument
                if (!(sInAr.Length < 2 || string.IsNullOrEmpty(sInAr[1])))          // If the string is empty, the argument stays 0, else we try and parse it
                    int.TryParse(sInAr[1], out arg);                                // If the parsing fails args stays 0

                string sIns = sInAr[0].ToLowerInvariant();
                ins = set.FindIndex(i => i.Name == sIns);

                if (ins == -1)
                    throw new Exception($"PspspsParser couldn't parse instruction `{sIns}` at line: {i}");

                instructions.Add((byte)ins);
                arguments.Add(arg);
            }

            return new PspspsCode(instructions.ToArray(), arguments.ToArray());
        }

    }
}
