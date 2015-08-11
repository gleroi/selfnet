using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    internal interface IHttpGateway
    {
        Task<JObject> Get(string url);
    }

    internal class HttpGateway : IHttpGateway
    {
        public async Task<JObject> Get(string url)
        {
            var http = new HttpClient();
            var resp = await http.GetAsync(url);
            if (resp.IsSuccessStatusCode)
            {
                var str = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(str);
                return json;
            }
            throw new HttpRequestException(resp.ReasonPhrase + "(" + resp.StatusCode + ")");
        }
    }
}