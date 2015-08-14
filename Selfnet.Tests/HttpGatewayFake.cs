using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Selfnet.Tests
{
    class HttpGatewayFake : IHttpGateway
    {
        public List<JObject> GetResponses { get; private set; }

        public HttpGatewayFake()
        {
            this.GetResponses = new List<JObject>();
        }

        public void GetReturns(string jsonResponse)
        {
            this.GetResponses.Add(JObject.Parse(jsonResponse));
        }

        public int GetCounter { get; private set; }

        public Task<JContainer> Get(string url)
        {
            var response = this.GetResponses[this.GetCounter];
            this.GetCounter += 1;
            return Task<JContainer>.FromResult(response as JContainer);
        }
    }
}
