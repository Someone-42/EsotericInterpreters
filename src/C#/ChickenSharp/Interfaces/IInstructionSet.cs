using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSharp.Interfaces
{
    public interface IInstructionSet
    {

        public Action<int>[] instructions { get; set; }

        /// <summary>
        /// Executes the instruction using the VM
        /// </summary>
        /// <param name="instruction"></param>
        /// <param name="arg"></param>
        /// <param name="vm"></param>
        public void Execute(int instruction, int? arg, IVM vm);
    }
}
