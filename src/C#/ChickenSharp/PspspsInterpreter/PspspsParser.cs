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

        public static string IntToPspspsBinary(int i)
        {
            char[] ps = new char[2] { 's', 'p' };
            char[] sc = new char[32];
            int pos = 31;
            while (i > 0)
            {
                sc[pos--] = ps[i & 1];
                i = i >> 1;
            }
            if (pos == 31)
                return "";
            return new string(sc, pos + 1, 31 - pos);
        }

        public static PspspsCode CodeFromString(string code, PspspsInstructionSet instructionSet)
        {
            string[] sInstructions = code.Split(new char[]{ '\n', ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<byte> instructions = new List<byte>(sInstructions.Length);         // Setting the capacity to the amount of lines, which will pretty much always be enough
            List<int> arguments = new List<int>(sInstructions.Length);

            List<PspspsInstruction> set = instructionSet.Instructions.ToList();

            Dictionary<string, int> labelKeys = new Dictionary<string, int>();
            List<int> labels = new List<int>();
            List<bool> labelIsFunction = new List<bool>();                         // List of bools, indicating if the label in the list, is for a function or a label

            bool inMultilineComment = false;

            // TODO: Analyze first line
            // First line has information regarding the language type and version
            // Also contains Preffered Memory size and Function stack size

            for(int i = 1; i < sInstructions.Length; i++)
            {
                // I could use state machines for instruction parsing, but im lazy

                string sInstruction = sInstructions[i].ToLowerInvariant().Trim();
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

                // The whole instruction is a comment
                string commentBegin = "";
                if (sInstruction.StartsWith("//")) commentBegin = "//";
                else if (sInstruction.StartsWith("/*")) commentBegin = "*/";

                if (commentBegin != "") // Removing comment from line
                {
                    int commentStart = sInstruction.IndexOf(commentBegin);
                    if (commentBegin == "/*") inMultilineComment = true;
                    continue;
                }

                string commentType = "";
                if (sInstruction.Contains("//")) commentType = "//";
                else if (sInstruction.Contains("/*")) commentType = "*/";

                if (commentType != "") // Removing comment from line
                {
                    int commentStart = sInstruction.IndexOf(commentType);
                    if (commentType == "/*") inMultilineComment = true;
                    sInstruction = sInstruction.Substring(0, commentStart);
                }
                #endregion Comments

                string[] sInAr = sInstruction.ToLowerInvariant().Trim().Split(' '); // Returns an array, with the instruction to decode, and the argument
                string sIns = sInAr[0];

                if (string.IsNullOrEmpty(sIns)) continue;                           // Skipping if there are no instructions

                string sAr = "";                                                    // String instruction
                if (sInAr.Length > 1 && !(string.IsNullOrEmpty(sInAr[1])))          // Skipping srting argument if it doenst exist
                    sAr = sInAr[1];                                                 // Getting string argument

                ins = set.FindIndex(i => i.Name == sIns);

                if (ins == -1)
                    throw new Exception($"PspspsParser couldn't parse instruction `{sIns}` at line: {i}");

                if (sAr.Length == 0)
                    arg = null;
                #region LabelsAndFunctions
                // If the current line is a label or function definition
                else if (ins == instructionSet.LabelInstructionIndex || ins == instructionSet.FunctionInstructionIndex)
                {
                    bool isFunction = ins == instructionSet.FunctionInstructionIndex;
                    if (labelKeys.ContainsKey(sAr))                                 // If the label is already defined
                    {
                        int labelIndex = labelKeys[sAr];
                        if (labels[labelIndex] >= 0)                                // If the index of the label doens't exist yet
                            throw new Exception($"The label or function {sAr} is already defined in the previous lines (Be careful as label and function names are shared)");
                        // If the label is called from an Execute instruction
                        if ((!isFunction) && labelIsFunction[labelIndex])
                            throw new Exception($"The label `{sAr}` is referenced by a function call instruction, label at line: {i} ({set[instructionSet.LabelInstructionIndex].Name})");
                        // If the function is called from a Goto instruction
                        if (isFunction && !labelIsFunction[labelIndex])
                            throw new Exception($"The function `{sAr}` is referenced by a goto instruction, function at line: {i} ({set[instructionSet.FunctionInstructionIndex].Name})");

                        labels[labelIndex] = instructions.Count();
                        arg = labelIndex;
                    }
                    else
                    {
                        labelKeys.Add(sAr, labels.Count());                         // Create the label key if it doens't exist
                        arg = labels.Count();
                        labels.Add(instructions.Count());                           // Add the label, with index as key, and value being the instruction at which you have to come back
                        labelIsFunction.Add(ins == instructionSet.FunctionInstructionIndex);
                    }
                    
                }
                // Goto or execute instruction -> the arg has to be the label index
                else if (ins == instructionSet.GotoInstructionIndex || ins == instructionSet.ExecuteInstructionIndex)
                {
                    bool isFunction = ins == instructionSet.ExecuteInstructionIndex;
                    if (labelKeys.ContainsKey(sAr))                                 // If the label is already defined
                    {
                        int labelIndex = labelKeys[sAr];
                        arg = labelIndex;
                        // If it's a function label, and it is called by a Goto instruction
                        if ((!isFunction) && labelIsFunction[labelIndex])
                            throw new Exception($"The function label `{sAr}` is referenced by a Goto instruction, at line: {i} ({set[instructionSet.GotoInstructionIndex].Name})");
                        // if it's a label, but it is called by a function execution instruction
                        if (isFunction && !labelIsFunction[labelIndex])
                            throw new Exception($"The label `{sAr}` is referenced by a function call instruction, at line: {i} ({set[instructionSet.FunctionInstructionIndex].Name})");
                    }
                    else
                    {
                        labelKeys.Add(sAr, labels.Count());                         // Create the label key if it doens't exist
                        arg = labels.Count();
                        labels.Add(-1);                                             // Add a non defined position of the Label
                        labelIsFunction.Add(isFunction);
                    }
                }
                #endregion LabelsAndFunctions
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

            if (labels.Contains(-1)) throw new Exception($"No matching label definition was found for {labelKeys.First(l => l.Value == labels.IndexOf(-1)).Key}");

            return new PspspsCode(instructions.ToArray(), arguments.ToArray(), labels.ToArray(), instructionSet.GetKey());
        }

    }
}
