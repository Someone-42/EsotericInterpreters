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

        public int instructionPointer { get; set; } = 2;

        public ChickenVM(IInstructionSet instructionSet)
        {
            this.instructionSet = instructionSet;
            stack = new Stack();

            stack.Insert(this.stack, 0);
            stack.Insert("", 1);
        }

        public object GetNextInstruction()
        {
            return stack.GetAt(instructionPointer++);
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
                    object instructionInfo = GetNextInstruction();
                    if (instructionInfo is int[] ia)
                    {
                        if (ia.Length > 1)
                            instructionSet.Execute(ia[0], ia[1], this);
                        else
                            instructionSet.Execute(ia[0], null, this);
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

        public void Stop()
        {
            instructionPointer = stack.Length;
        }

    }
}
