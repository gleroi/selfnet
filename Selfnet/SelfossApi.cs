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

        private readonly IHttpGateway http;

        internal SelfossApi(ConnectionOptions opts, IHttpGateway http)
        {
            this.http = http;
            this.Options = opts;
        }

        public SelfossApi(ConnectionOptions opts)
            : this(opts, new HttpGateway())
        { }

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
            var json = await this.http.Get(url);
            JToken token;
            return json.TryGetValue("success", out token) && token.Value<bool>();
        }
    }
}