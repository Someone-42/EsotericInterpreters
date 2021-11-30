using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSharp.Interpreter
{
    public interface IInstructionSet
    {
        public void Execute(IVM vm);
    }
}
