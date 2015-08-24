using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    internal interface IHttpGateway
    {
        Task<JContainer> Get(string url);
        Task<JContainer> Post(string url);
        Task<JContainer> Post(string url, IEnumerable<KeyValuePair<string, string>> parameters);
    }

    internal class HttpGateway : IHttpGateway
    {

        public async Task<JContainer> Get(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            var resp = await http.SendAsync(req);
            this.EnsureSuccessOrThrow(resp);
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }

        public async Task<JContainer> Post(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var resp = await http.SendAsync(req);
            this.EnsureSuccessOrThrow(resp);
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }

        public async Task<JContainer> Post(string url, IEnumerable<KeyValuePair<string, string>> parameters)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var content = new FormUrlEncodedContent(parameters);
            req.Content = content;
            var resp = await http.SendAsync(req);
            this.EnsureSuccessOrThrow(resp);
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }

        private async void EnsureSuccessOrThrow(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode)
            {
                return;
            }
            if (resp.StatusCode == HttpStatusCode.BadRequest || resp.StatusCode == HttpStatusCode.InternalServerError)
            {
                var msg = await resp.Content.ReadAsStringAsync();
                throw new SelfossServerException(msg, null);
            }
            resp.EnsureSuccessStatusCode();
        }
    }
}