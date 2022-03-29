using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class FixedStack<T> : IEnumerable
    {
        private T[] array;
        private int pointer;

        public readonly int Capacity;

        public FixedStack(int capacity)
        {
            array = new T[capacity];
            pointer = -1;
            Capacity = capacity;
        }

        public void Push(T element)
        {
            array[++pointer] = element;
        }

        public T Pop()
        {
            return array[pointer--];
        }

        public int Count()
        {
            return pointer + 1;
        }
        
        public bool IsEmpty()
        {
            return pointer == -1;
        }

        public void Clear()
        {
            pointer = -1;
        }

        public T Peek(int index)
        {
            if (index > pointer || index < 0)
                throw new ArgumentOutOfRangeException("index");
            return array[index];
        }

        public void Set(T value, int index)
        {
            if (index > pointer || index < 0)
                throw new ArgumentOutOfRangeException("index");
            array[index] = value;
        }

        //TODO: add getat and setat operators like fs[i] = v

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new FixedStackEnumerator<T>(array, pointer);
        }
    }
}
