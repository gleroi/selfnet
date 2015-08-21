using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    public class SourcesApi : BaseApi
    {
        internal SourcesApi(ConnectionOptions opts, IHttpGateway http)
            : base(opts, http)
        { }

        public async Task<IEnumerable<Source>> Get()
        {
            var url = this.BuildUrl("sources/list");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            return json.ToObject<List<Source>>();
        }

        public async Task<IEnumerable<SourceStat>> Stats()
        {
            var url = this.BuildUrl("sources/stats");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            return json.ToObject<List<SourceStat>>();
        }

        public async Task<IEnumerable<Spout>> Spouts()
        {
            var url = this.BuildUrl("sources/spouts");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            return ReadSpouts(json);
        }

        private IEnumerable<Spout> ReadSpouts(JContainer json)
        {
            var input = json.ToObject<JObject>();
            var spouts = new List<Spout>();

            foreach (var prop in input.Properties())
            {
                var value = prop.Value.ToObject<JObject>();
                var spout = new Spout()
                {
                    Id = prop.Name,
                    Description = value["description"].ToString(),
                    Name = value["name"].ToString(),
                    Params = this.ReadDescriptors(value["params"]),
                };
                spouts.Add(spout);
            }

            return spouts;
        }

        private Dictionary<string, ParameterDescriptor> ReadDescriptors(JToken token)
        {
            if (token.Type == JTokenType.Boolean)
            {
                return new Dictionary<string, ParameterDescriptor>();
            }
            else if (token.Type == JTokenType.Object)
            {
                return token.ToObject<Dictionary<string, ParameterDescriptor>>();
            }
            throw new ArgumentException("Unknow value for Spout.Params");
        }
    }
}
