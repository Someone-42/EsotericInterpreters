using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Esoterics.Interfaces;
using Esoterics.PspspsInterpreter;

namespace Esoterics.InstructionSets
{
    public static class PspspsV1
    {
        public const string NAME = "PSPSPSx?";
        public const string VERSION = "0";

        public static PspspsInstructionSet Set => NewSetCopy();
        
        public static PspspsInstructionSet NewSetCopy()
        {
            return new PspspsInstructionSet
                (
                NAME, VERSION,
                new PspspsInstruction[14]
                {
                    new PspspsInstruction("psh", Push, true),
                    new PspspsInstruction("pop", Pop),
                    new PspspsInstruction("eql", Equality),
                    new PspspsInstruction("cmp", Compare),
                    new PspspsInstruction("add", Add),
                    new PspspsInstruction("sub", Sub),
                    new PspspsInstruction("mul", Mul),
                    new PspspsInstruction("div", Div),
                    new PspspsInstruction("jmp", Jump),
                    new PspspsInstruction("gto", Goto, true),
                    new PspspsInstruction("lbl", Label, true),
                    new PspspsInstruction("abs", Abs),
                    new PspspsInstruction("cpy", Copy),
                    new PspspsInstruction("wrt", Write)
                },
                10,
                9,
                (s) => 
                {
                    if (int.TryParse(s, out int v))
                        return v;
                    throw new Exception($"Couldn't parse integer argument : {s}");
                }
                );
        }

        public static void Write(int arg, PspspsVM vm)
        {
            Console.WriteLine(vm.Memory.Pop());
        }

        public static void Push(int arg, PspspsVM vm)
        {
            vm.Memory.Push(arg);
        }

        public static void Pop(int arg, PspspsVM vm)
        {
            vm.Memory.Pop();
        }

        public static void Jump(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            if (a2 != 0)                       // If top of stack is truthy
                vm.InstructionPointer += a1;   // Jump of offset before-top of stack
        }
        
        public static void Goto(int arg, PspspsVM vm)
        {
            vm.InstructionPointer = vm.Code.LabelAddresses[arg];
        }

        public static void Compare(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            if (a1 == a2)
            {
                vm.Memory.Push(0);
            }
            else if (a1 > a2)
            {
                vm.Memory.Push(1);
            }
            else
            {
                vm.Memory.Push(-1);
            }
        }

        public static void Div(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a1 / a2);
        }

        public static void Mul(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a1 * a2);
        }

        public static void Add(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a1 + a2);
        }

        public static void Abs(int arg, PspspsVM vm)
        {
            vm.Memory.Push(Math.Abs(vm.Memory.Pop()));
        }

        public static void Copy(int arg, PspspsVM vm)
        {
            vm.Memory.Push(vm.Memory.Peek(vm.Memory.Count() - vm.Memory.Pop() - 1));
        }

        public static void Sub(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a1 - a2);
        }

        public static void Label(int arg, PspspsVM vm) { return; }

        public static void Equality(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a1 == a2 ? 1 : 0);
        }
    }
}
