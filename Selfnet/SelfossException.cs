using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet
{
    public class SelfossException : Exception
    {
        public SelfossException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
