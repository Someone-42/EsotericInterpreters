using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Esoterics.PspspsInterpreter;

namespace Esoterics.InstructionSets
{
    public static class PspspsV1
    {
        public const string NAME = "PSPSPSx18";
        public const string VERSION = "1.0";

        public static PspspsInstructionSet Set => NewSetCopy();

        public static PspspsInstructionSet NewSetCopy()
        {
            return new PspspsInstructionSet
                (
                NAME, VERSION,
                new PspspsInstruction[22]
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
                    new PspspsInstruction("fnc", Function),
                    new PspspsInstruction("ret", Return),
                    new PspspsInstruction("exe", Execute),
                    new PspspsInstruction("ext", Exit)
                },
                10,
                9,
                14,
                16,
                (s) =>
                {
                    if (int.TryParse(s, out int v))
                        return v;
                    throw new Exception($"Couldn't parse integer argument : {s}");
                }
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
            int a1 = vm.Memory.Pop(), a2 = vm.Memory.Pop();
            int m1 = vm.Memory.Peek(vm.Memory.Count() - a1), m2 = vm.Memory.Peek(vm.Memory.Count() - a2);
            vm.Memory.Set(a1, m2);
            vm.Memory.Set(a2, m1);
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

    }
}
