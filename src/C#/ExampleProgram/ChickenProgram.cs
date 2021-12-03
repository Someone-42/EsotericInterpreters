using System;
using System.Collections.Generic;
using System.Text;
using ChickenSharp;
using ChickenSharp.InstructionSets;
using ChickenSharp.Interfaces;
using ChickenSharp.Interpreter;

namespace ExampleProgram
{
    public static class ChickenProgram
    {
        /// <summary>
        /// Writes hellow world using Chicken
        /// </summary>
        public static void HelloWorld()
        {

            ChickenVM vm = new ChickenVM(ChickenSharpV1.Set);

            ChickenCode code = Parser.CodeFromFile("chicken_helloworld.cks", ChickenSharpV1.Set);

            vm.Execute(code);

            Console.WriteLine(vm.stack.Pop());

        }
    }
}
