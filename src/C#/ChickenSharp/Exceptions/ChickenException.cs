using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenSharp.Exceptions
{
    /// <summary>
    /// Represents an Exception occuring in the execution of chicken
    /// </summary>
    [Serializable]
    public class ChickenException : Exception
    {
        public ChickenException() : base("Error in executing Chicken Code")
        { }

        public ChickenException(string reason) :
            base($"{reason}")
        { }

        public ChickenException(string reason, Exception innerException) :
            base($"{reason} - {innerException.Message}")
        { }

        public ChickenException(Exception innerException) :
            base($"Error in executing Chicken Code : {innerException.Message}")
        { }
    }
}
