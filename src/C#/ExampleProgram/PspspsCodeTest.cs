using Esoterics.InstructionSets;
using Esoterics.PspspsInterpreter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public static void RunFibonnaci()
        {
            PspspsVM vm = new PspspsVM(PspspsV1.Set);
            string scode = File.ReadAllText("DynamicFib.psp");
            PspspsCode code = PspspsParser.CodeFromString(scode, PspspsV1.Set);
            Stopwatch sw = new Stopwatch();
            sw.Restart();
            vm.Execute(code);
            sw.Stop();
            Console.WriteLine($"total instructions : {vm.TotalInstructionsExecuted}, took : {sw.ElapsedMilliseconds}ms");
            Console.ReadKey();

        }

        public static void RunHashing(string s)
        {
            PspspsVM vm = new PspspsVM(PspspsV1.Set);
            string scode = File.ReadAllText("Hashing.psp");
            PspspsCode code = PspspsParser.CodeFromString(scode, PspspsV1.Set);

            foreach (char c in s)
                vm.Memory.Push(c);

            Stopwatch sw = new Stopwatch();
            sw.Restart();
            vm.Execute(code);
            sw.Stop();
            Console.WriteLine("Hash is : " + vm.Memory.Peek(0));
            Console.WriteLine($"total instructions : {vm.TotalInstructionsExecuted}, took : {sw.ElapsedMilliseconds}ms");
        }

        public static void ConvertToPspsps(string fileName, string outputFile)
        {
            string scode = File.ReadAllText(fileName);
            PspspsCode code = PspspsParser.CodeFromString(scode, PspspsV1.Set);
            File.WriteAllText(outputFile, PspspsUtils.GetCodeAsText(code, PspspsV1.GetPspspsPspspsSet()));
        }

    }
}
