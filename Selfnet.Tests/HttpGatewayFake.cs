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
        public List<JContainer> ServerResponses { get; private set; }
        public int ServerCounter { get; private set; }
        public void ServerReturns(string jsonResponse)
        {
            this.ServerResponses.Add(JsonConvert.DeserializeObject<JContainer>(jsonResponse));
        }

        public HttpGatewayFake()
        {
            this.ServerResponses = new List<JContainer>();
        }


        public Task<JContainer> Get(string url)
        {
            var response = this.ServerResponses[this.ServerCounter];
            this.ServerCounter += 1;
            return Task.FromResult(response);
        }

        public Task<JContainer> Post(string url)
        {
            var response = this.ServerResponses[this.ServerCounter];
            this.ServerCounter += 1;
            return Task.FromResult(response);
        }

        public Task<JContainer> Post(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var response = this.ServerResponses[this.ServerCounter];
            this.ServerCounter += 1;
            return Task.FromResult(response);
        }

        public Task<JContainer> Delete(string url)
        {
            var response = this.ServerResponses[this.ServerCounter];
            this.ServerCounter += 1;
            return Task.FromResult(response);
        }
    }
}
