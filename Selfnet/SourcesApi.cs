﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    public interface ISourcesApi {
        Task<IEnumerable<Source>> Get();
        Task<bool> Save(Source source);
        Task<bool> Delete(int id);
        Task<IEnumerable<SourceStat>> Stats();
        Task<IEnumerable<Spout>> Spouts();
    }

    public class SourcesApi : BaseApi, ISourcesApi
    {
        internal SourcesApi(ConnectionOptions opts, IHttpGateway http)
            : base(opts, http)
        { }

        public async Task<IEnumerable<Source>> Get()
        {
            var url = this.BuildUrl("sources/list");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            return this.ReadSources(json);
        }

        private IEnumerable<Source> ReadSources(JContainer json)
        {
            var sources =  new List<Source>();
            var items = json.ToObject<List<JObject>>();
            foreach (var item in items)
            {
                var source = new Source()
                {
                    Id = item["id"].Value<int>(),
                    Title = item["title"].ToString(),
                    Spout = item["spout"].ToString(),
                    Params = item["params"].ToObject<Dictionary<string, string>>(),
                    Error = item["error"].ToString(),
                    Favicon = item["icon"].ToString(),
                    Tags = new List<string>(item["tags"].ToString().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                };
                sources.Add(source);
            }
            return sources;
        }

        public async Task<bool> Save(Source source)
        {
            var url = this.BuildUrl("source");
            if (source.Id != 0)
            {
                url = this.BuildUrl("source/" + source.Id);
            }
            var parameters = new Dictionary<string, string>();
            parameters["title"] = source.Title;
            parameters["spout"] = source.Spout;
            parameters["tags"] = String.Join(",", source.Tags);
            foreach (var spoutParameter in source.Params)
            {
                parameters.Add(spoutParameter.Key, spoutParameter.Value);
            }
            var json = await this.Http.Post(url.Uri.AbsoluteUri, parameters);

            JToken token;
            json.ToObject<JObject>().TryGetValue("id", out token);

            var id = token.Value<int>();
            source.Id = id;

            return this.ReadSuccess(json);
        }

        public async Task<bool> Delete(int id)
        {
            var url = this.BuildUrl("source/" + id);
            var json = await this.Http.Delete(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
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
