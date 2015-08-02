using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    public class SelfossApi
    {
        public ConnectionOptions Options { get; set; }

        public SelfossApi(ConnectionOptions opts)
        {
            Options = opts;
        }

        private KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        private static async Task<JObject> Get(UriBuilder url)
        {
            var http = new HttpClient();
            var resp = await http.GetAsync(url.Uri);
            if (resp.IsSuccessStatusCode)
            {
                var str = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(str);
                return json;
            }
            throw new HttpRequestException(resp.ReasonPhrase + "(" + resp.StatusCode + ")");
        }

        private UriBuilder BuildUrl(string path)
        {
            var url = BuildRoot(Options);
            url.Path = Options.Base + "/" + path;
            return url;
        }

        private UriBuilder BuildRoot(ConnectionOptions options)
        {
            var url = new UriBuilder(options.Scheme, options.Host, options.Port);
            return url;
        }

        public async Task<bool> Login()
        {
            var url = BuildUrl("login");
            url.Query = "username=" + Options.Username + "&" + "password=" + Options.Password;
            var json = await Get(url);
            JToken token;
            return json.TryGetValue("success", out token) && token.Value<bool>();
        }
    }
}