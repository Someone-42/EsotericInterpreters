using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSharp.Interfaces
{
    public interface IVM
    {
        public IStack stack { get; }

        public IInstructionSet instructionSet { get; set; }

        public object GetNextInstruction();

        public void Execute(ChickenCode code, object userInput = null);

        /// <summary>
        /// Stops the execution
        /// </summary>
        public void Stop();

    }
}
