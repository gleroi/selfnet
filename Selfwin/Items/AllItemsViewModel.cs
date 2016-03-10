using System.Threading.Tasks;
using Caliburn.Micro;
using Selfwin.Core;
using Selfwin.Selfoss;
using Selfwin.Shell;
using SelfwinApp = Selfwin.Selfoss.SelfwinApp;

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


        private BindableCollection<IItemViewModel> _items;

        public BindableCollection<IItemViewModel> Items
        {
            get { return _items; }
            set
            {
                if (Equals(value, _items)) return;
                _items = value;
                NotifyOfPropertyChange();
            }
        }

        private BindableCollection<IItemViewModel> _unreadItems;
        public BindableCollection<IItemViewModel> UnreadItems
        {
            get { return _unreadItems; }
            set
            {
                if (Equals(value, _unreadItems)) return;
                _unreadItems = value;
                NotifyOfPropertyChange();
            }
        }

        private BindableCollection<IItemViewModel> _starredItems;
        public BindableCollection<IItemViewModel> StarredItems
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
            this.Items = new BindableCollection<IItemViewModel>(items);
            var unread = await this.App.UnreadItems();
            this.UnreadItems = new BindableCollection<IItemViewModel>(unread);
            var starred = await this.App.StarredItems();
            this.StarredItems = new BindableCollection<IItemViewModel>(starred);
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