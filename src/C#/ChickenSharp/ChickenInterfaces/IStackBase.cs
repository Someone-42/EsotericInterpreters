using System;
using System.Collections.Generic;
using System.Text;

namespace Esoterics.Interfaces
{
    public interface IStackBase<T>
    {
        public T Pop();
        public void Push(T item);
        public int Count();

    }
}
