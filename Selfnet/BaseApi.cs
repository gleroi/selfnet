using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    public class BaseApi
    {
        public ConnectionOptions Options { get; set; }

        internal readonly IHttpGateway Http;

        private static readonly IList<JsonConverter> Converters;

        static BaseApi()
        {
            BaseApi.Converters = new List<JsonConverter>
            {
                new SelfossBoolConverter(),
            };
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = BaseApi.Converters
            };
        }

        internal BaseApi(ConnectionOptions opts, IHttpGateway http)
        {
            this.Options = opts;
            this.Http = http;
        }

        protected UriBuilder BuildUrl(string path, string query = null)
        {
            var url = BuildRoot(Options, query);
            url.Path = Options.Base + "/" + path;
            return url;
        }

        protected UriBuilder BuildRoot(ConnectionOptions options, string query = null)
        {
            var url = new UriBuilder(options.Scheme, options.Host, options.Port);
            string login = "username=" + Options.Username + "&" + "password=" + Options.Password;
            if (!String.IsNullOrEmpty(query))
            {
                login += "&" + query;
            }
            url.Query = login;
            return url;
        }

        protected bool ReadSuccess(JContainer json)
        {
            JToken token;
            return json.ToObject<JObject>().TryGetValue("success", out token) && token.Value<bool>();
        }
    }
}
