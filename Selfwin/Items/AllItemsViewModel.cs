using System.Threading.Tasks;
using Caliburn.Micro;
using Selfwin.Selfoss;
using Selfwin.Shell;

namespace Selfwin.Items
{
    public class AllItemsViewModel : Screen
    {
        private SelfwinApp App { get; }
        private IAppNavigation Navigation { get; }

        public AllItemsViewModel(IAppNavigation navigation, SelfwinApp app)
        {
            this.App = app;
            this.Navigation = navigation;
        }


        private BindableCollection<ItemViewModel> _items;

        public BindableCollection<ItemViewModel> Items
        {
            get { return _items; }
            set
            {
                if (Equals(value, _items)) return;
                _items = value;
                NotifyOfPropertyChange();
            }
        }

        private BindableCollection<ItemViewModel> _unreadItems;
        public BindableCollection<ItemViewModel> UnreadItems
        {
            get { return _unreadItems; }
            set
            {
                if (Equals(value, _unreadItems)) return;
                _unreadItems = value;
                NotifyOfPropertyChange();
            }
        }

        private BindableCollection<ItemViewModel> _starredItems;
        public BindableCollection<ItemViewModel> StarredItems
        {
            get { return _starredItems; }
            set
            {
                if (Equals(value, _starredItems)) return;
                _starredItems = value;
                NotifyOfPropertyChange();
            }
        }

        protected override async void OnActivate()
        {
            base.OnActivate();
            await ReadItems();
        }

        private async Task ReadItems()
        {
            var items = await this.App.Items();
            this.Items = new BindableCollection<ItemViewModel>(items);
            var unread = await this.App.UnreadItems();
            this.UnreadItems = new BindableCollection<ItemViewModel>(unread);
            var starred = await this.App.StarredItems();
            this.StarredItems = new BindableCollection<ItemViewModel>(starred);
        }

        public void OnItemSelected(ItemViewModel item)
        {
            this.Navigation.NavigateTo<ReadItemViewModel>(item);
        }

        public async void Refresh()
        {
            await this.App.Refresh();
            await this.ReadItems();
        }
    }
}