using ChickenSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenSharp.Exceptions;

namespace ChickenSharp.Interpreter
{
    public class ChickenVM : IVM
    {
        public IStack stack { get; }
        public IInstructionSet instructionSet { get; set; }

        private int instructionPointer = 2;

        public ChickenVM(IInstructionSet instructionSet, IStack stack)
        {
            this.instructionSet = instructionSet;
            this.stack = stack;

            this.stack.Insert(this.stack, 0);
            this.stack.Insert("", 1);
        }

        public void Execute(ChickenCode code, object userInput = null)
        {
            //User input
            if (userInput is null)
                userInput = "";
            stack.SetAt(1, userInput);

            //Adding code to stack
            stack.Extend(code.instructions);

            //Push exit instruction (Admitting exit is 0 no matter the instruction set)
            stack.Push(0);

            //Main loop
            while (instructionPointer < stack.Length)
            {
                try
                {
                    object instructionInfo = stack.GetAt(instructionPointer++);
                    if (instructionInfo is int[] ia)
                    {
                        instructionSet.Execute(ia[0], ia[1], this);
                    }
                    else if (instructionInfo is int i)
                    {
                        instructionSet.Execute(i, null, this);
                    }
                }
                catch(Exception e)
                {
                    throw new ChickenException(e);
                }
            }

        }
    }
}
