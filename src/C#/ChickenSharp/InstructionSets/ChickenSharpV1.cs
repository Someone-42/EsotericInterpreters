using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChickenSharp.Exceptions;
using ChickenSharp.Interfaces;
using ChickenSharp.Interpreter;

namespace ChickenSharp.InstructionSets
{
    public static class ChickenSharpV1
    {

        public const string NAME = "CSx11";
        public const string VERSION = "1.0";

        public static IInstructionSet Set = NewSetCopy();

        public static IInstructionSet NewSetCopy() => new InstructionSet(NAME, VERSION,
            new Instruction[11]
            {
                new Instruction("exit", Exit),
                new Instruction("chicken", Chicken),
                new Instruction("add", Add),
                new Instruction("subtract", Subtract),
                new Instruction("multiply", Multiply),
                new Instruction("compare", Compare),
                new Instruction("load", Load),
                new Instruction("store", Store),
                new Instruction("jump", Jump),
                new Instruction("char", Char),
                new Instruction("push", Push),
            }
        );

        internal static void Exit(int? arg, IVM vm)
        {
            vm.Stop();
        }

        internal static void Chicken(int? arg, IVM vm)
        {
            vm.stack.Push("chicken");
        }

        internal static void Add(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();
            if (loperand is int li && roperand is int ri)
                vm.stack.Push(li + ri);
            else
                vm.stack.Push($"{loperand}{roperand}");
        }

        internal static void Subtract(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();
            if (loperand is int li && roperand is int ri)
                vm.stack.Push(li - ri);
            else
                throw new ChickenException("Cannot subtract 2 strings");
        }

        internal static void Multiply(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();
            if (loperand is int li)
            {
                if (roperand is int ri)
                    vm.stack.Push(li * ri);
                else if (roperand is string s)
                    vm.stack.Push(Enumerable.Repeat(s, li));
            }
            else if (loperand is string s && roperand is int ri)
                vm.stack.Push(Enumerable.Repeat(s, ri));
            else
                throw new ChickenException($"Multiplication is not valid on types : {roperand.GetType()} and {loperand.GetType()}");
        }

        internal static void Compare(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();
            vm.stack.Push(roperand.ToString() == loperand.ToString());
        }

        internal static void Load(int? arg, IVM vm)
        {
            object nextToken = vm.GetNextInstruction();
            int loadFrom = 0;
            if (nextToken is int[] a && a[0] == 1) loadFrom = 1;
            else if (nextToken is int i && i == 1) loadFrom = 1;

            try
            {
                if (loadFrom == 0) 
                    vm.stack.Push(vm.stack.GetAt((int) vm.stack.Pop())); // User loads from stack, at index pop
                else
                {
                    vm.stack.Push((vm.stack.GetAt(0) as string)[(int)vm.stack.Pop()]); // User wants to load from input, gathering the char -> string[index]
                }
            }
            catch
            {
                vm.stack.Push(""); //Couldn't load the value so we push an empty value
            }
        }

        internal static void Store(int? arg, IVM vm)
        {
            object o = vm.stack.Pop();
            if (o is int index)
                vm.stack.SetAt(index, vm.stack.Pop());
            else
                throw new ChickenException($"Cannot store value {vm.stack.Pop()} at index {o} because it is not an int");
        }

        internal static void Jump(int? arg, IVM vm)
        {
            object o = vm.stack.Pop();
            if (o is int offset)
            {
                object condition = vm.stack.Pop();
                if ((condition is bool b && b) || (condition is int i && i != 0) || (condition is string s && s.Length > 0)) //If the condition is truthy
                    vm.instructionPointer += offset;
            }
            else
                throw new ChickenException($"Cannot Jump using offset {o} because it is not an int");
        }

        internal static void Char(int? arg, IVM vm)
        {
            object o = vm.stack.Pop();
            if (o is int i)
                vm.stack.Push(((char)i).ToString()); // Converting char to string so additions work - or at least less errors
            else if (o is string || o is char) vm.stack.Push(o); // Already a string, we dont do anything
            else
                throw new ChickenException($"Couldn't convert {o} to char");
        }

        internal static void Push(int? arg, IVM vm)
        {
            vm.stack.Push(arg ?? 0);
        }
    }
}
