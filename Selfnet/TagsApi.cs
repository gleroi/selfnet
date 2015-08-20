using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet
{
    public class TagsApi : BaseApi
    {
        internal TagsApi(ConnectionOptions opts, IHttpGateway http)
            : base(opts, http)
        { }

        public async Task<IEnumerable<Tag>> Get()
        {
            var url = this.BuildUrl("tags");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            var result = json.ToObject<List<Tag>>();
            return result;
        }
    }
}
