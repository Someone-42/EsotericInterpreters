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

        [TestMethod]
        public void Test2()
        {
            FixedStack<int> stack = new FixedStack<int>(10);
            stack.Push(42);
            stack.Push(42);
            stack.Push(69);
            stack.Push(420);
            stack.Push(666);
            stack.Push(13);
            Assert.AreEqual(stack.Peek(3), 420);
            try
            {
                stack.Peek(42);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e is IndexOutOfRangeException);
            }
        }

        //TODO: Tests for every function of the stack

    }
}
