using Esoterics.ChickenInterpreter;
using Esoterics.InstructionSets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esoterics.Interfaces
{
    public interface IInstructionSet
    {

        /// <summary>
        /// Name of the Instruction Set
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Version of the Instruction Set, can be used to check for compatibility
        /// </summary>
        public string Version { get; }

        public Instruction[] Instructions { get; set; }

        /// <summary>
        /// Executes the instruction using the VM
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="arg"></param>
        /// <param name="vm"></param>
        public void Execute(int instruction, int? arg, IVM vm);

        public string GetKey();
    }
}
