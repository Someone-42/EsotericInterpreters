using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Esoterics.Interfaces;
using System.Linq;

namespace Esoterics
{
    public class Stack : IStack
    {
        public int Length { get => stack.Count; }

        private List<object> stack = new List<object>();

        public void Extend<T>(T[] elements)
        {
            stack.AddRange(elements.Cast<object>());
        }

        public void Extend<T>(T[] elements, int startIndex)
        {
            stack.InsertRange(startIndex, elements.Cast<object>());
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
            stack.Add(element);
        }

        public void SetAt(int index, object element)
        {
            stack[index] = element;
        }

        public object Last()
        {
            return stack.Last();
        }

    }
}
