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
                throw new IndexOutOfRangeException("index");
            return array[index];
        }

        public void Set(T value, int index)
        {
            if (index > pointer || index < 0)
                throw new IndexOutOfRangeException("index");
            array[index] = value;
        }

        public void Extend(int count, T value)
        {
            Extend(pointer, count, value);
        }

        public void Extend(int startIndex, int count, T value)
        {
            if (count + startIndex >= Capacity)
                throw new StackOverflowException("Count goes over the stack capacity");
            for (int i = 0; i < count; i++)
                array[startIndex + i] = value;
        }

        //TODO: add getat and setat operators like fs[i] = v

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public FixedStackEnumerator<T> GetEnumerator()
        {
            return new FixedStackEnumerator<T>(array, pointer);
        }

    }
}
