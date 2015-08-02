using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Selfnet.Tests
{
    static class Context
    {
        private static Mock<IHttpGateway> http;
        private static SelfossApi api;

        public static SelfossApi Api()
        {
            return null;
        }

        public static void HttpGetReturns(string login, string successTrue)
        {
            throw new NotImplementedException();
        }
    }
}
