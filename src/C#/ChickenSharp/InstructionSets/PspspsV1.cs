using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Esoterics.PspspsInterpreter;

namespace Esoterics.InstructionSets
{
    public static class PspspsV1
    {
        public const string NAME = "PSPSPSx28";
        public const string VERSION = "1.4";

        public static PspspsInstructionSet Set => NewSetCopy();

        public static PspspsInstructionSet NewSetCopy()
        {
            return new PspspsInstructionSet
                (
                NAME, VERSION,
                new PspspsInstruction[28]
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
                    new PspspsInstruction("wrt", Write),
                    new PspspsInstruction("cat", CopyAt),
                    new PspspsInstruction("jat", JumpAt),
                    new PspspsInstruction("cip", CurrentInstructionPointer),
                    new PspspsInstruction("swp", Swap),
                    new PspspsInstruction("fnc", Function, true),
                    new PspspsInstruction("ret", Return),
                    new PspspsInstruction("exe", Execute, true),
                    new PspspsInstruction("chr", WriteChar),
                    new PspspsInstruction("sat", SetAt),
                    new PspspsInstruction("all", StackAlloc),
                    new PspspsInstruction("mod", Modulo),
                    new PspspsInstruction("set", SetWithOffset),
                    new PspspsInstruction("mps", MemoryPosition),
                    new PspspsInstruction("ext", Exit)
                },
                10,
                9,
                18,
                20,
                (s) =>
                {
                    if (int.TryParse(s, out int v))
                        return v;
                    throw new Exception($"Couldn't parse integer argument : {s}");
                },
                (i) => i.ToString()
                );
        }

        public static readonly Dictionary<string, string> PspspsTranslatedKeywords = new Dictionary<string, string>()
        {
            { "psh", "ps"       },
            { "pop", "psp"      },
            { "eql", "psps"     },
            { "cmp", "sp"       },
            { "add", "p"        },
            { "sub", "s"        },
            { "mul", "pp"       },
            { "div", "ss"       },
            { "jmp", "ppp"      },
            { "lbl", "pspsps"   },
            { "gto", "spspsp"   },
            { "abs", "sps"      },
            { "cpy", "pssp"     },
            { "wrt", "pspspsps" },
            { "ext", "ssss"     },
            { "cat", "psspss"   },
            { "jat", "pppss"    },
            { "cip", "pppp"     },
            { "swp", "psppss"   },
            { "fnc", "pspspss"  },
            { "ret", "spspspp"  },
            { "chr", "pspsspsp" },
            { "sat", "pspss"    },
            { "all", "psss"     },
            { "mod", "psssp"    },
            { "set", "psppsppsp"},
            { "mps", "pssspsppsspsp"},
            { "exe", "sspspsp"  }
        };

        public static PspspsInstructionSet GetPspspsPspspsSet()
        {
            PspspsInstructionSet set = NewSetCopy();
            for (int i = 0; i < set.Instructions.Length; i++)
            {
                PspspsInstruction instruction = set.Instructions[i];
                set.Instructions[i] = new PspspsInstruction(PspspsTranslatedKeywords[instruction.Name], instruction.Method, instruction.SupportsArgument);
            }
            set.ParseArgumentMethod = PspspsParser.PspspsBinaryToInt;
            set.ReverseParseArgumentMethod = PspspsParser.IntToPspspsBinary;
            return set;
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
            if (a1 > a2)
                vm.Memory.Push(1);
            else if (a1 == a2)
                vm.Memory.Push(0);
            else
                vm.Memory.Push(-1);
        }

        public static void Div(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a2 / a1);
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
            vm.Memory.Push(a2 - a1);
        }

        public static void Label(int arg, PspspsVM vm) { return; }

        public static void Equality(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a1 == a2 ? 1 : 0);
        }

        public static void CopyAt(int arg, PspspsVM vm)
        {
            vm.Memory.Push(vm.Memory.Peek(vm.Memory.Pop()));
        }

        public static void JumpAt(int arg, PspspsVM vm)
        {
            vm.InstructionPointer = vm.Memory.Pop();
        }

        public static void CurrentInstructionPointer(int arg, PspspsVM vm)
        {
            vm.Memory.Push(vm.InstructionPointer);
        }

        public static void Exit(int arg, PspspsVM vm)
        {
            vm.InstructionPointer = vm.Code.Instructions.Length; // Puts the instruction pointer over the limit, making the vm quit execution
        }

        public static void Swap(int arg, PspspsVM vm)
        {
            int c = vm.Memory.Count() - 3;
            int a1 = c - vm.Memory.Pop(), 
                a2 = c - vm.Memory.Pop();
            int m1 = vm.Memory.Peek(a1), m2 = vm.Memory.Peek(a2);
            vm.Memory.Set(m2, a1);
            vm.Memory.Set(m1, a2);
        }

        public static void Function(int arg, PspspsVM vm) { return; }

        public static void Execute(int arg, PspspsVM vm)
        {
            vm.FunctionStack.Push(vm.InstructionPointer);
            vm.InstructionPointer = vm.Code.LabelAddresses[arg];
        }

        public static void Return(int arg, PspspsVM vm)
        {
            vm.InstructionPointer = vm.FunctionStack.Pop();
        }

        public static void WriteChar(int arg, PspspsVM vm)
        {
            Console.Write((char)vm.Memory.Pop());
        }

        public static void StackAlloc(int arg, PspspsVM vm)
        {
            vm.Memory.Extend(vm.Memory.Pop(), 0);
        }

        public static void SetAt(int arg, PspspsVM vm)
        {
            vm.Memory.Set(vm.Memory.Pop(), vm.Memory.Pop());
        }

        public static void Modulo(int arg, PspspsVM vm)
        {
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            vm.Memory.Push(a2 % a1);
        }

        public static void SetWithOffset(int arg, PspspsVM vm)
        {
            int offset = vm.Memory.Count() - vm.Memory.Pop() - 2;
            vm.Memory.Set(vm.Memory.Pop(), offset);
        }

        public static void MemoryPosition(int arg, PspspsVM vm)
        {
            vm.Memory.Push(vm.Memory.Count());
        }

    }
}
