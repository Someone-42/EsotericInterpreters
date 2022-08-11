using Esoterics.InstructionSets;
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

        public readonly int LabelInstructionIndex;
        public readonly int GotoInstructionIndex;

        public readonly int FunctionInstructionIndex;
        public readonly int ExecuteInstructionIndex;

        public Func<string, int> ParseArgumentMethod;
        public Func<int, string> ReverseParseArgumentMethod;

        public PspspsInstructionSet(string name, string version, 
            PspspsInstruction[] instructions, 
            int labelInstructionIndex, int gotoInstructionIndex,
            int functionInstructionIndex, int executeInstructionIndex,
            Func<string, int> parseArgumentMethod = null,
            Func<int, string> reverseParseArgumentMethod = null)
        {
            Instructions = instructions;
            Name = name;
            Version = version;
            LabelInstructionIndex = labelInstructionIndex;
            GotoInstructionIndex = gotoInstructionIndex;
            FunctionInstructionIndex = functionInstructionIndex;
            ExecuteInstructionIndex = executeInstructionIndex;
            if (parseArgumentMethod is null)
                ParseArgumentMethod = PspspsParser.PspspsBinaryToInt;
            else
                ParseArgumentMethod = parseArgumentMethod;
            if (reverseParseArgumentMethod is null)
                ReverseParseArgumentMethod = PspspsParser.IntToPspspsBinary;
            else
                ReverseParseArgumentMethod = reverseParseArgumentMethod;

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
