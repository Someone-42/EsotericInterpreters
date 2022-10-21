using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public struct PspspsHeaderSettings
    {
        // ex of first line : # lang=PSPSPSx28, version=1.4, memsize4=1024, fss=1024, type=asm

        public PspspsInstructionSet InstructionSet;
        public string TargetVersion;
        public int MemSize;
        public int FSS;
        public string type;

        public bool HeaderFound;

        public PspspsHeaderSettings(PspspsInstructionSet instructionSet, string targetVersion, int memSize, int fSS, string type, bool headerFound = true)
        {
            InstructionSet = instructionSet;
            TargetVersion = targetVersion;
            MemSize = memSize;
            FSS = fSS;
            this.type = type;
            HeaderFound = headerFound;
        }

        public string GetHeaderString()
        {
            return $"# lang={InstructionSet.Name}, version={TargetVersion}, memsize4={MemSize}, fss={FSS}, type={type}";
        }

    }
}
