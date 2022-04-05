using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public struct PspspsInstruction
    {
        public string Name;
        public Action<int, PspspsVM> Method;

        public PspspsInstruction(string name, Action<int, PspspsVM> method)
        {
            Name = name;
            Method = method;
        }
    }
}
