﻿using System.Collections.Generic;
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
    }

    internal class HttpGateway : IHttpGateway
    {

        public async Task<JContainer> Get(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            var resp = await http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }

        public async Task<JContainer> Post(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var resp = await http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }
    }
}