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

        internal SelfossApi(ConnectionOptions opts, IHttpGateway http)
            : base(opts, http)
        {
            this.Items = new ItemsApi(opts, http);
        }

        public SelfossApi(ConnectionOptions opts)
            : base(opts, new HttpGateway()) {}

        public async Task<bool> Login()
        {
            var url = BuildUrl("login");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }
    }
}