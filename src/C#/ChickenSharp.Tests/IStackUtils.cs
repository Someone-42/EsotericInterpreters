using System;
using System.Collections.Generic;
using System.Text;
using Esoterics.Interfaces;

namespace Esoterics.Tests
{
    public static class IStackUtils
    {
        public static IStack Fill(IStack stack)
        {
            stack.Push("42");
            stack.Push(42);
            stack.Push("Banana");
            return stack;
        }

        public static bool CheckFill(IStack s)
        {
            return s.GetAt(0) as string == "42" && (int)s.GetAt(1) == 42 && s.GetAt(2) as string == "Banana";
        }

    }
}
