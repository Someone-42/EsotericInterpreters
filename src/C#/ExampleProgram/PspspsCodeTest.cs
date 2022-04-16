using Esoterics.InstructionSets;
using Esoterics.PspspsInterpreter;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleProgram
{
    public static class PspspsCodeTest
    {
        public static void ExampleProgram()
        {
            PspspsVM vm = new PspspsVM(PspspsV1.Set);
            string scode = "psh 42;lbl psps;psh 1;cpy;wrt;psh 1;psh 0;sub;add;psh 1;cpy;psh 1;eql;psh 1;jmp;gto psps";
            PspspsCode code = PspspsParser.CodeFromString(scode, PspspsV1.Set);
            vm.Execute(code);
            Console.WriteLine(vm.Memory.Pop());
        }
    }
}
