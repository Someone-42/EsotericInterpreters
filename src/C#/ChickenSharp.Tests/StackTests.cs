using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Esoterics.Tests
{
    [TestClass]
    public class StackTests
    {
        [TestMethod]
        public void StackContainsStuff()
        {
            Stack s = IStackUtils.Fill(new Stack()) as Stack;

            Assert.IsTrue(IStackUtils.CheckFill(s));
        }

        [TestMethod]
        public void StackExtend()
        {
            Stack s = IStackUtils.Fill(new Stack()) as Stack;

            int i = s.Length;

            int[] array = new int[4] { 4, 2, 6, 9 };
            s.Extend(array);

            Assert.IsTrue((int)s.GetAt(i + 0) == array[0]);
            Assert.IsTrue((int)s.GetAt(i + 1) == array[1]);
            Assert.IsTrue((int)s.GetAt(i + 2) == array[2]);
            Assert.IsTrue((int)s.GetAt(i + 3) == array[3]);

        }

        [TestMethod]
        public void StackExtendIndex()
        {
            Stack s = new Stack();
            s.Push("42");
            s.Push(42);
            s.Push("Banana");

            int[] array = new int[4] { 4, 2, 6, 9 };

            s.Extend(array, 0);
            //Iterate over inserted elements in IStackUtils
            Assert.IsTrue((int)s.GetAt(0) == array[0]);
            Assert.IsTrue((int)s.GetAt(1) == array[1]);
            Assert.IsTrue((int)s.GetAt(2) == array[2]);
            Assert.IsTrue((int)s.GetAt(3) == array[3]);

        }

        [TestMethod]
        public void StackSetAtAndInsert()
        {
            Stack s = new Stack();
            s.Push("42");
            s.Push(42);
            s.Push("Banana");

            s.SetAt(1, 69);
            s.Insert("abc", 1);

            Assert.IsTrue(s.GetAt(0) as string == "42");
            Assert.IsTrue(s.GetAt(1) as string == "abc");
            Assert.IsTrue((int)s.GetAt(2) == 69);
            Assert.IsTrue(s.GetAt(3) as string == "Banana");

        }

        [TestMethod]
        public void StackLength()
        {
            Stack s = new Stack();

            Assert.IsTrue(s.Length == 0);

            s.Push("42");
            s.Push(42);
            s.Push("Banana");

            Assert.IsTrue(s.Length == 3);

            s.Pop();

            Assert.IsTrue(s.Length == 2);

        }

    }
}
