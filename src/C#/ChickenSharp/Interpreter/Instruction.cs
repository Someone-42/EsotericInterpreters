using ChickenSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSharp.Interpreter
{
    public struct Instruction
    {
        public string Name;

        public Action<int?, IVM> Method;

        public Instruction(string name, Action<int?, IVM> method)
        {
            Name = name.ToLowerInvariant();
            Method = method;
        }

    }
}
