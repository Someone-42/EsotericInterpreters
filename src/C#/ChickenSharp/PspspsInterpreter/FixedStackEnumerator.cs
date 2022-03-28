using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.PspspsInterpreter
{
    public class FixedStackEnumerator<T> : IEnumerator
    {

        private T[] array;
        private int actualSize;
        private int pointer = -1;

        public FixedStackEnumerator(T[] array, int pointer)
        {
            this.array = array;
            actualSize = pointer + 1; // The Fixed stack has a pointer defining the position of the last element in the stack -> this position + 1 is the number of elements
        }

        public T Current => array[pointer];

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            pointer++;
            return pointer < actualSize;
        }

        public void Reset()
        {
            pointer = -1;
        }
    }
}
