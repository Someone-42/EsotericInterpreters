using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSharp.Interfaces
{
    public interface IVM
    {
        public IStack stack { get; set; }

        public IInstructionSet instructionSet { get; set; }

        public void Execute(ChickenCode code);

    }
}
