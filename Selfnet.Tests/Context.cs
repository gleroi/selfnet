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
        private static SelfossApi api;

        public static ConnectionOptions Options;

        public static readonly HttpGatewayFake Http = new HttpGatewayFake();

        public static SelfossApi Api()
        {
            if (api == null)
            {
                api = new SelfossApi(Options, Http);
            }
            return api;
        }
    }
}
