using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Esoterics.InstructionSets;

namespace Esoterics.PspspsInterpreter
{
    public static class PspspsParser
    {
        /// <summary>
        /// Converts a string like "pspspsps" to 10101010, we assume the string only contains p and s, and is all lowercase
        /// </summary>
        /// <param name="pspsps"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static int PspspsBinaryToInt(string pspsps)
        {
            if (pspsps.Length > 32)
                throw new Exception("Cannot parse binary number larger than 32 bits");
            int value = 0;
            char c;
            for (int i = pspsps.Length - 1; i >= 0; i--)    // Going from Length - 1 to 0 (included)
            {
                c = pspsps[i];                         // Looking up string in reverse
                if (c == 'p')
                    value += 1 << i;                        // Adding the corresponding bit value at position i
            }
            return value;
        }

        public static PspspsCode CodeFromString(string code, PspspsInstructionSet instructionSet)
        {
            string[] sInstructions = code.Split(new char[]{ '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<byte> instructions = new List<byte>(sInstructions.Length);         // Setting the capacity to the amount of lines, which will pretty much always be enough
            List<int> arguments = new List<int>(sInstructions.Length);

            List<PspspsInstruction> set = instructionSet.Instructions.ToList();

            Dictionary<string, int> labelKeys = new Dictionary<string, int>();
            List<int> labels = new List<int>();

            bool inMultilineComment = false;

            for(int i = 0; i < sInstructions.Length; i++)
            {
                string sInstruction = sInstructions[i];
                int ins = -1;
                int? arg = null;

                #region Comments
                if (inMultilineComment)
                {
                    if (!sInstruction.Contains("*/"))
                        continue;

                    sInstruction = sInstruction.Substring(sInstruction.IndexOf("*/"));

                }

                if (sInstruction.StartsWith("//")) continue;                        // Skipping if it is a comment
                else if (sInstruction.StartsWith("/*"))                             // Skipping if it is a multiline comment
                {
                    inMultilineComment = true;
                    continue;
                }

                string commentBegin = "";
                if (sInstruction.StartsWith("//")) commentBegin = "//";
                else if (sInstruction.StartsWith("/*")) commentBegin = "*/";

                if (commentBegin != "") // Removing comment from line
                {
                    int commentStart = sInstruction.IndexOf(commentBegin);
                    if (commentBegin == "/*") inMultilineComment = true;
                    sInstruction = sInstruction.Substring(0, commentStart);
                }
                #endregion Comments

                string[] sInAr = sInstruction.ToLowerInvariant().Trim().Split(' '); // Returns an array, with the instruction to decode, and the argument
                string sIns = sInAr[0];

                if (string.IsNullOrEmpty(sIns)) continue;                                     // Skipping if there are no instructions

                string sAr = "";                                                    // String instruction
                if (sInAr.Length > 1 && !(string.IsNullOrEmpty(sInAr[1])))          // Skipping srting argument if it doenst exist
                    sAr = sInAr[1];                                                 // Getting string argument

                ins = set.FindIndex(i => i.Name == sIns);

                if (ins == -1)
                    throw new Exception($"PspspsParser couldn't parse instruction `{sIns}` at line: {i}");

                if (sAr.Length == 0)
                    arg = null;
                #region LabelAndGoto
                else if (ins == instructionSet.LabelInstructionIndex)               // If the current line is a label instruction
                {
                    if (labelKeys.ContainsKey(sAr))                                 // If the label is already defined
                    {
                        int labelIndex = labelKeys[sIns];
                        if (labels[labelIndex] >= 0)                                // If the index of the label doens't exist yet
                            throw new Exception($"The label {sAr} is already defined in the previous lines");
                        labels[labelIndex] = instructions.Count();
                        arg = labelIndex;
                    }
                    else
                    {
                        labelKeys.Add(sAr, labels.Count());                         // Create the label key if it doens't exist
                        arg = labels.Count();
                        labels.Add(instructions.Count());                           // Add the label, with index as key, and value being the instruction at which you have to come back
                    }
                    
                }
                else if (ins == instructionSet.GotoInstructionIndex)                // Goto instruction -> the arg has to be the label index
                {
                    if (labelKeys.ContainsKey(sAr))                                 // If the label is already defined
                    {
                        arg = labelKeys[sAr];
                    }
                    else
                    {
                        labelKeys.Add(sAr, labels.Count());                         // Create the label key if it doens't exist
                        arg = labels.Count();
                        labels.Add(-1);                                             // Add a non defined position of the Label 
                    }
                }
                #endregion LabelAndGoto
                else
                {
                    arg = instructionSet.ParseArgumentMethod(sAr);
                }

                #region ArgChecking
                if (arg is null && set[ins].SupportsArgument) throw new Exception($"Instruction missing argument on `{set[ins].Name}` instruction, line : {i}");
                if (!(arg is null) && !set[ins].SupportsArgument) throw new Exception($"The instruction {set[ins].Name} doesnt support arguments, line : {i}");
                #endregion ArgChecking

                instructions.Add((byte)ins);
                arguments.Add(arg ?? 0);
            }

            if (labels.Contains(-1)) throw new Exception($"No matching label definition was found for {labels.IndexOf(-1)}");

            return new PspspsCode(instructions.ToArray(), arguments.ToArray(), labels.ToArray(), instructionSet.GetKey());
        }

    }
}
