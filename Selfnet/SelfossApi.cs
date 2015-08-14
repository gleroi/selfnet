using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    public class SelfossApi
    {
        public ConnectionOptions Options { get; set; }

        private readonly IHttpGateway http;

        private readonly IList<JsonConverter> Converters;

        internal SelfossApi(ConnectionOptions opts, IHttpGateway http)
        {
            this.http = http;
            Options = opts;
            Converters = new List<JsonConverter> { new SelfossBoolConverter() };
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = Converters
            };
        }

        public SelfossApi(ConnectionOptions opts)
            : this(opts, new HttpGateway())
        {
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
            url.Query = "username=" + Options.Username + "&" + "password=" + Options.Password;
            return url;
        }

        public async Task<bool> Login()
        {
            var url = BuildUrl("login");
            var json = await http.Get(url.Uri.AbsoluteUri);
            JToken token;
            return json.ToObject<JObject>().TryGetValue("success", out token) && token.Value<bool>();
        }

        public async Task<IEnumerable<Item>> Items()
        {
            var url = BuildUrl("items");
            var json = await http.Get(url.Uri.AbsoluteUri);
            var result = json.ToObject<List<Item>>();
            return result;
        }
    }
}