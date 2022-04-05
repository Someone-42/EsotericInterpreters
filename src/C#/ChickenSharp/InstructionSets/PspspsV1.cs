using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Esoterics.Interfaces;

namespace Esoterics.InstructionSets
{
    public static class PspspsV1
    {

        public const string NAME = "PSPSPSx?";
        public const string VERSION = "0";

        public static IInstructionSet Set => NewSetCopy();
        
        public static IInstructionSet NewSetCopy()
        {
            return null;
        }


    }
}
