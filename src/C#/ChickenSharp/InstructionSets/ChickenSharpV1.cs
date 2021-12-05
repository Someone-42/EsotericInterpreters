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
        public const string VERSION = "1.1"; // Not fully working, some implementations and fixes remain to be done - simplification of code as well

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

        public static void Exit(int? arg, IVM vm)
        {
            vm.Stop();
        }

        public static void Chicken(int? arg, IVM vm)
        {
            vm.stack.Push("chicken");
        }

        public static void Add(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();
            if (TryParseStackValue(loperand, out int li) && TryParseStackValue(roperand, out int ri))
                vm.stack.Push(li + ri);
            else
                vm.stack.Push($"{loperand}{roperand}");
        }

        public static void Subtract(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();

            if (TryParseStackValue(loperand, out int li) && TryParseStackValue(roperand, out int ri))
                vm.stack.Push(li - ri);
            else
                throw new ChickenException($"These two types are not subtractable {roperand.GetType()} `{roperand}` and {loperand.GetType()} `{loperand}`");
        }

        public static void Multiply(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();
            if (TryParseStackValue(loperand, out int li))
            {
                if (TryParseStackValue(roperand, out int ri))
                    vm.stack.Push(li * ri);
                else if (roperand is string s)
                    vm.stack.Push(Enumerable.Repeat(s, li));
            }
            else if (loperand is string s && TryParseStackValue(roperand, out int ri))
                vm.stack.Push(Enumerable.Repeat(s, ri));
            else
                throw new ChickenException($"Multiplication is not valid on types : {roperand.GetType()} and {loperand.GetType()}");
        }

        public static void Compare(int? arg, IVM vm)
        {
            object roperand = vm.stack.Pop();
            object loperand = vm.stack.Pop();
            vm.stack.Push(roperand.ToString() == loperand.ToString());
        }

        public static void Load(int? arg, IVM vm)
        {
            object nextToken = vm.GetNextInstruction();

            if(!TryParseStackValue(nextToken, out int loadFrom)) throw new ChickenException($"Cannot load from {nextToken} because it is not convertable to int");

            try
            {
                TryParseStackValue(vm.stack.Pop(), out int index);
                if (loadFrom == 0)
                    vm.stack.Push(vm.stack.GetAt(index > 0 ? index : vm.stack.Length - index)); // User loads from stack, at index pop
                else
                {
                    string s = vm.stack.GetAt(0) as string;
                    vm.stack.Push(s[index > 0 ? index : s.Length - index]); // User wants to load from input, gathering the char -> string[index]
                }
            }
            catch
            {
                vm.stack.Push(""); //Couldn't load the value so we push an empty value
            }
        }

        public static void Store(int? arg, IVM vm)
        {
            object o = vm.stack.Pop();
            if (o is int index)
                vm.stack.SetAt(index, vm.stack.Pop());
            else
                throw new ChickenException($"Cannot store value {vm.stack.Pop()} at index {o} because it is not an int");
        }

        public static void Jump(int? arg, IVM vm)
        {
            object o = vm.stack.Pop();
            if (o is int offset)
            {
                object condition = vm.stack.Pop();
                if ((condition is bool b && b) || (TryParseStackValue(condition, out int i) && i != 0) || (condition is string s && s.Length > 0)) //If the condition is truthy
                    vm.instructionPointer += offset;
            }
            else
                throw new ChickenException($"Cannot Jump using offset {o} because it is not an int");
        }

        public static void Char(int? arg, IVM vm)
        {
            object o = vm.stack.Pop();
            if (TryParseStackValue(o, out int i))
                vm.stack.Push(((char)i).ToString());
            else if (o is string || o is char) vm.stack.Push(o); // Already a string, we dont do anything
            else
                throw new ChickenException($"Couldn't convert {o} to char");
        }

        public static void Push(int? arg, IVM vm)
        {
            vm.stack.Push(arg ?? 0);
        }

        public static bool TryParseStackValue(object stackValue, out int i)
        {
            if (stackValue is int ii)
            {
                i = ii;
                return true;
            }
            else if (stackValue is int[] ia)
            {
                i = ia.Sum();
                return true;
            }
            i = 0;
            return false;
        }

    }
}
