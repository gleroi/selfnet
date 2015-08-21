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

        public async Task<bool> ChangeColor(string tag, string color)
        {
            var url = this.BuildUrl("tags/color");
            var json = await this.Http.Post(url.Uri.AbsoluteUri, new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("tag", tag),
                new KeyValuePair<string, string>("color", color),
            });
            return this.ReadSuccess(json);
        }
    }
}
