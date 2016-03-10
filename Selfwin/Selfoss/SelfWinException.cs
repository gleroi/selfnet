using System;

namespace Selfwin.Selfoss
{
    public class SelfWinException : Exception
    {
        public SelfWinException(string message)
            : base(message)
        { }
    }
}