using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet.Tests
{
    class HttpGatewayFake : IHttpGateway
    {
        public List<JContainer> GetResponses { get; private set; }

        public HttpGatewayFake()
        {
            this.GetResponses = new List<JContainer>();
        }

        public void GetReturns(string jsonResponse)
        {
            this.GetResponses.Add(JsonConvert.DeserializeObject<JContainer>(jsonResponse));
        }

        public int GetCounter { get; private set; }

        public Task<JContainer> Get(string url)
        {
            var response = this.GetResponses[this.GetCounter];
            this.GetCounter += 1;
            return Task.FromResult(response);
        }
    }
}
