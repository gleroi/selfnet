using System;
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
        Task<JContainer> Delete(string url);
    }

    internal class HttpGateway : IHttpGateway
    {

        public async Task<JContainer> Get(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            var resp = await http.SendAsync(req);
            await this.EnsureSuccessOrThrow(resp);
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }

        public async Task<JContainer> Post(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            req.Content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("ajax", "true"),
            });
            var resp = await http.SendAsync(req);
            await this.EnsureSuccessOrThrow(resp);
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
            await this.EnsureSuccessOrThrow(resp);
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }

        public async Task<JContainer> Delete(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Delete, url);
            var resp = await http.SendAsync(req);
            await this.EnsureSuccessOrThrow(resp);
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }

        private async Task<bool> EnsureSuccessOrThrow(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode)
            {
                return true;
            }
            if (resp.StatusCode == HttpStatusCode.BadRequest || resp.StatusCode == HttpStatusCode.InternalServerError)
            {
                var msg = await resp.Content.ReadAsStringAsync();
                throw new SelfossServerException(msg, null);
            }
            resp.EnsureSuccessStatusCode();
            return true;
        }
    }
}