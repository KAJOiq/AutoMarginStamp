using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class MarginalizationException : Exception
    {
        public MarginalizationException(string message) : base(message) { }
        public MarginalizationException(string message, Exception innerException)
        : base(message, innerException) { }
    }
}
