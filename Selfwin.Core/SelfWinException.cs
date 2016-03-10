using System;

namespace Selfwin.Core
{
    public class SelfWinException : Exception
    {
        public SelfWinException(string message)
            : base(message)
        { }
    }
}