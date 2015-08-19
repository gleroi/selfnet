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
        public int GetCounter { get; private set; }

        public HttpGatewayFake()
        {
            this.GetResponses = new List<JContainer>();
            this.PostResponses = new List<JContainer>();
        }

        public void GetReturns(string jsonResponse)
        {
            this.GetResponses.Add(JsonConvert.DeserializeObject<JContainer>(jsonResponse));
        }

        public Task<JContainer> Get(string url)
        {
            var response = this.GetResponses[this.GetCounter];
            this.GetCounter += 1;
            return Task.FromResult(response);
        }

        public List<JContainer> PostResponses { get; private set; }
        public int PostCounter { get; private set; }

        public void PostReturns(string jsonResponse)
        {
            this.PostResponses.Add(JsonConvert.DeserializeObject<JContainer>(jsonResponse));
        }

        public Task<JContainer> Post(string url)
        {
            var response = this.PostResponses[this.PostCounter];
            this.PostCounter += 1;
            return Task.FromResult(response);
        }

        public Task<JContainer> Post(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var response = this.PostResponses[this.PostCounter];
            this.PostCounter += 1;
            return Task.FromResult(response);
        }
    }
}
