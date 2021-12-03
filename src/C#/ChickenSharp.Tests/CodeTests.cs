using ChickenSharp.InstructionSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ChickenSharp.Interfaces;
using ChickenSharp.Interpreter;

namespace ChickenSharp.Tests
{
    [TestClass]
    public class CodeTests
    {
        [TestMethod]
        public void CodeWorks1()
        {
            ChickenCode code = Parser.CodeFromString("push 24\npush 3\nsubtract\npush 2\nmultiply\nexit", ChickenSharpV1.Set);

            ChickenVM vm = new ChickenVM(ChickenSharpV1.Set);

            vm.Execute(code);

            Assert.AreEqual(vm.stack.Last(), 42);

        }
    }
}
