using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSharp.Interfaces
{
    public interface IStack
    {
        public object Pop();
        public void Push(object element);
        public void Insert(object element, int index);

        public void GetAt(int index);

    }
}
