using Esoterics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esoterics.ChickenInterpreter
{
    public struct ChickenInstruction
    {
        public string Name;

        public Action<int?, IVM> Method;

        public ChickenInstruction(string name, Action<int?, IVM> method)
        {
            Name = name.ToLowerInvariant();
            Method = method;
        }

    }
}
