﻿using Esoterics.ChickenInterpreter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esoterics.ChickenInterfaces
{
    public interface IVM
    {
        public IStack stack { get; }

        public IInstructionSet instructionSet { get; set; }

        public int instructionPointer { get; set; }

        public object GetNextInstruction();

        public void Execute(ChickenCode code, object userInput = null);

        /// <summary>
        /// Stops the execution
        /// </summary>
        public void Stop();

    }
}
