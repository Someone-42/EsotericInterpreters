using Esoterics.PspspsInterpreter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.Tests
{
    [TestClass]
    public class FixedStackTests
    {

        [TestMethod]
        public void SimpleTest1()
        {
            FixedStack<int> stack = new FixedStack<int>(10);
            stack.Push(42);
            int value = stack.Pop();
            Assert.IsTrue(value == 42);
        }

    }
}
