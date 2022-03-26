using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esoterics.Interfaces
{
    public interface IStack
    {

        public int Length { get; }

        public object Pop();
        public void Push(object element);
        public void Insert(object element, int index);

        public object GetAt(int index);

        public void SetAt(int index, object element);

        public void Extend<T>(T[] elements);

        public void Extend<T>(T[] elements, int startIndex);

        public object Last();

    }
}
