using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    internal interface IHttpGateway
    {
        Task<JContainer> Get(string url);
    }

    internal class HttpGateway : IHttpGateway
    {

        public async Task<JContainer> Get(string url)
        {
            var http = new HttpClient();
            var req = new HttpRequestMessage(HttpMethod.Get, url);
            req.Headers.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_8_2) AppleWebKit/537.17 (KHTML, like Gecko) Chrome/24.0.1309.0 Safari/537.17");
            var resp = await http.SendAsync(req);
            resp.EnsureSuccessStatusCode();
            var str = await resp.Content.ReadAsStringAsync();
            var json = JsonConvert.DeserializeObject<JContainer>(str);
            return json;
        }
    }
}