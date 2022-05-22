﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public static class PspspsUtils
    {

        public static string GetCodeAsText(PspspsCode code, PspspsInstructionSet set)
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < code.Instructions.Length; i++)
            {
                byte ins = code.Instructions[i];
                int arg = code.Arguments[i];
                builder.AppendLine($"{set.Instructions[ins].Name} {set.ReverseParseArgumentMethod(arg)}");
            }

            return builder.ToString();
        }

    }
}
