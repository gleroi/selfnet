using System.Threading.Tasks;

namespace Selfnet
{
    public interface ISelfossApi {
        IItemsApi Items { get; }
        ITagsApi Tags { get; }
        ISourcesApi Sources { get; }
        Task<bool> Login();
    }

    public class SelfossApi : BaseApi, ISelfossApi
    {
        public IItemsApi Items { get; private set; }
        public ITagsApi Tags { get; private set; }
        public ISourcesApi Sources { get; private set; }

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