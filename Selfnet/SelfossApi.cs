using System;
using System.Collections.Generic;
using System.Net;
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

        KeyValuePair<string, string> Pair(string key, string value)
        {
            return new KeyValuePair<string, string>(key, value);
        }

        public async Task<bool> Login()
        {
            var url = BuildUrl("login");
            var http = new HttpClient();
            url.Query = "username=" + this.Options.Username + "&" + "password=" + this.Options.Password;
            var resp = await http.GetAsync(url.Uri);
            if (resp.IsSuccessStatusCode)
            {
                var str = await resp.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<JObject>(str);
                JToken token;
                if (json.TryGetValue("success", out token))
                {
                    return token.Value<bool>();
                }
            }
            return false;
        }

        private UriBuilder BuildUrl(string path)
        {
            var url = BuildRoot(this.Options);
            url.Path = this.Options.Base + "/" + path;
            return url;
        }

        private UriBuilder BuildRoot(ConnectionOptions options)
        {
            var url = new UriBuilder(options.Scheme, options.Host, options.Port);
            return url;
        }
    }
}