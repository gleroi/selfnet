using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    public class SelfossApi
    {
        public ConnectionOptions Options { get; set; }

        private readonly IHttpGateway http;

        private readonly IList<JsonConverter> converters;

        internal SelfossApi(ConnectionOptions opts, IHttpGateway http)
        {
            this.Options = opts;
            this.http = http;
            this.converters = new List<JsonConverter> { new SelfossBoolConverter() };
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Converters = this.converters
            };
        }

        public SelfossApi(ConnectionOptions opts)
            : this(opts, new HttpGateway())
        {
        }

        private UriBuilder BuildUrl(string path, string query = null)
        {
            var url = BuildRoot(Options, query);
            url.Path = Options.Base + "/" + path;
            return url;
        }

        private UriBuilder BuildRoot(ConnectionOptions options, string query = null)
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

        private bool ReadSuccess(JContainer json)
        {
            JToken token;
            return json.ToObject<JObject>().TryGetValue("success", out token) && token.Value<bool>();
        }

        public async Task<bool> Login()
        {
            var url = BuildUrl("login");
            var json = await http.Get(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }

        public async Task<IEnumerable<Item>> Items()
        {
            var url = BuildUrl("items");
            var json = await http.Get(url.Uri.AbsoluteUri);
            var result = json.ToObject<List<Item>>();
            return result;
        }

        public async Task<IEnumerable<Item>> Items(ItemsFilter filter)
        {
            var parameters = filter.AsPairs();
            var query = String.Join("&", parameters.Select(pair => pair.Key + "=" + pair.Value));
            var url = BuildUrl("items", query);

            var json = await http.Get(url.Uri.AbsoluteUri);
            var result = json.ToObject<List<Item>>();
            return result;
        }

        public async Task<bool> MarkRead(int id)
        {
            var url = BuildUrl("mark/" + id);
            var json = await http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkAllRead(params int[] ids)
        {
            var url = BuildUrl("mark");
            var parameters = ids.Select(id => new KeyValuePair<string, string>("ids[]", id.ToString())).ToList();
            var json = await http.Post(url.Uri.AbsoluteUri, parameters);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkUnread(int id)
        {
            var url = BuildUrl("unmark/" + id);
            var json = await http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkStarred(int id)
        {
            var url = BuildUrl("starr/" + id);
            var json = await http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkUnstarred(int id)
        {
            var url = BuildUrl("unstarr/" + id);
            var json = await http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }
    }
}