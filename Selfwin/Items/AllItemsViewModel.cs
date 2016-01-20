
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Caliburn.Micro;
using Selfnet;

namespace Selfwin.Items
{
    public class AllItemsViewModel : Screen
    {
        ISelfossApi Api { get; }

        public AllItemsViewModel()
        {
            this.Api = new SelfossApi(new ConnectionOptions()
            {
                Host = "nostromo.myds.me",
                Base="selfoss",
                Username = "gleroi",
                Password="cXVa2I0L",
            });
        }

        private BindableCollection<ItemViewModel> _items;
        public BindableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set
            {
                if (Equals(value, _items)) return;
                _items = value;
                NotifyOfPropertyChange(() => Items);
            }
        }

        public BindableCollection<ItemViewModel> UnreadItems { get; set; }

        public BindableCollection<ItemViewModel> StarredItems { get; set; }

        protected override async void OnActivate()
        {
            base.OnActivate();
            var items = await this.Api.Items.Get(new ItemsFilter()
            {
            });
            var vms = items.Select(item => new ItemViewModel(item)).ToList();
            this.Items = new BindableCollection<ItemViewModel>(vms);
            this.UnreadItems = new BindableCollection<ItemViewModel>(vms.Where(v => v.Unread));
            this.StarredItems = new BindableCollection<ItemViewModel>(vms.Where(v => v.Starred));
        }
    }
}