using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
