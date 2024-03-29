﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esoterics.ChickenInterpreter
{
    /// <summary>
    /// Class that can contain ChickenCode as a universal format. Allows for translations.
    /// </summary>
    public class ChickenCode
    {

        public int[][] instructions;

        public ChickenCode(int[][] instructions)
        {
            this.instructions = new int[instructions.GetLength(0)][];
            instructions.CopyTo(this.instructions, 0); //NOW I DO
        }

        public static ChickenCode operator+ (ChickenCode c, ChickenCode co)
        {
            return new ChickenCode(c.instructions.Concat(co.instructions).ToArray());
        }

    }
}
