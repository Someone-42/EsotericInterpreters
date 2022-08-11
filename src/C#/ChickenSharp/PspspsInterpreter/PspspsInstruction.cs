using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public struct PspspsInstruction
    {
        public string Name;
        public Action<int, PspspsVM> Method;
        public bool SupportsArgument;

        public PspspsInstruction(string name, Action<int, PspspsVM> method, bool supportsArgument = false)
        {
            Name = name;
            Method = method;
            SupportsArgument = supportsArgument;
        }
    }
}
