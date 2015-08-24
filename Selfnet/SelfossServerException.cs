using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet
{
    public class SelfossServerException : Exception
    {
        public SelfossServerException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
