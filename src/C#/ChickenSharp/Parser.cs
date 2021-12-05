using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenSharp.Interfaces;
using ChickenSharp.Interpreter;
using System.IO;

namespace ChickenSharp
{
    public static class Parser
    {
        public static ChickenCode CodeFromFile(string path, IInstructionSet set)
        {
            return CodeFromString(File.ReadAllText(path), set);
        }

        public static ChickenCode CodeFromString(string s, IInstructionSet set)
        {
            string[] lines = s.Split('\n');
            List<int[]> code = new List<int[]>();

            List<Instruction> instructions = set.Instructions.ToList();

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim().ToLowerInvariant(); //Trims the line, and lowers it
                if (line.Contains("//")) line = line.Remove(line.IndexOf("//")).Trim(); // Removes comments

                if (line.Length == 0) continue; // Line is empty so we skip to next one

                string[] la = line.Split(' ');

                int instruction = instructions.FindIndex(i => i.Name == la[0]); // Search for the int representing the instruction in the Set, using the first member of the string array

                if (instruction == -1)
                    throw new Exception($"Couldn't read instruction `{line}` at line {i}, Maybe the instruction set is incompatible ?");

                if (la.Length > 1) code.Add(new int[2] { instruction, int.Parse(la[1]) }); // If the instruction has a parameter
                else code.Add(new int[1] { instruction }); // No paramater
                
            }

            return new ChickenCode(code.ToArray());
        }
    }
}
