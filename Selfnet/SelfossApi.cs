using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Selfnet
{
    public class SelfossApi : BaseApi
    {
        public ItemsApi Items { get; private set; }
        public TagsApi Tags { get; private set; }
        public SourcesApi Sources { get; set; }

        internal SelfossApi(ConnectionOptions opts, IHttpGateway http)
            : base(opts, http)
        {
            this.Items = new ItemsApi(opts, http);
            this.Tags = new TagsApi(opts, http);
            this.Sources = new SourcesApi(opts, http);
        }

        public SelfossApi(ConnectionOptions opts)
            : this(opts, new HttpGateway()) {}

        public async Task<bool> Login()
        {
            var url = BuildUrl("login");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }
    }
}