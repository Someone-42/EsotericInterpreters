using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenSharp.Interfaces;
using System.Linq;

namespace ChickenSharp
{
    public class Stack : IStack
    {
        public int Length { get => stack.Count; }

        private List<object> stack = new List<object>();

        public void Extend<T>(T[] elements)
        {
            stack.AddRange(elements as object[]);
        }

        public void Extend<T>(T[] elements, int startIndex)
        {
            stack.InsertRange(startIndex, elements as object[]);
        }

        public object GetAt(int index)
        {
            return stack[index];
        }

        public void Insert(object element, int index)
        {
            stack.Insert(index, element);
        }

        public object Pop()
        {
            object last = stack.Last();
            stack.RemoveAt(stack.Count - 1);
            return last;
        }

        public void Push(object element)
        {
            stack.Append(element);
        }

        public void SetAt(int index, object element)
        {
            stack[index] = element;
        }
    }
}
