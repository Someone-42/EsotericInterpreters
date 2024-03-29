﻿using System;
using System.Collections.Generic;
using System.Text;
using Esoterics;
using Esoterics.InstructionSets;
using Esoterics.ChickenInterfaces;
using Esoterics.ChickenInterpreter;

namespace ExampleProgram
{
    public static class ChickenPrograms
    {
        /// <summary>
        /// Writes hellow world using Chicken Sharp (CSx11 instruction set)
        /// </summary>
        public static void HelloWorld_CSx11()
        {

            ChickenVM vm = new ChickenVM(ChickenSharpV1.Set);

            ChickenCode code = ChickenParser.CodeFromFile("chicken_helloworld.cks", ChickenSharpV1.Set);

            vm.Execute(code);

            Console.WriteLine(vm.stack.Last());

        }


        /// <summary>
        /// Compares two numbers a, b using Chicken Sharp (CSx11 instruction set)
        /// </summary>
        public static void CompareAB_CSx11(int a, int b)
        {

            ChickenVM vm = new ChickenVM(ChickenSharpV1.Set);

            ChickenCode code = ChickenParser.CodeFromFile("is_a_greater_than_b.cki", ChickenSharpV1.Set);
            ChickenCode pushABValues = ChickenParser.CodeFromString($"push {a}\npush {b}", ChickenSharpV1.Set); // Push a, b on stack so they can be compared

            vm.Execute(pushABValues + code);

            Console.WriteLine(vm.stack.Last());

        }

    }
}
