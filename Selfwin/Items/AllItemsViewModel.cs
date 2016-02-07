using System.Linq;
using Caliburn.Micro;
using Selfnet;
using Selfwin.Selfoss;

namespace Selfwin.Items
{
    public class AllItemsViewModel : Screen
    {
        private SelfwinApp App { get; }
        private INavigationService Navigation { get; }

        public AllItemsViewModel(INavigationService navigation, SelfwinApp app)
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
                NotifyOfPropertyChange(() => Items);
            }
        }

        public BindableCollection<ItemViewModel> UnreadItems { get; set; }

        public BindableCollection<ItemViewModel> StarredItems { get; set; }

        protected override async void OnActivate()
        {
            base.OnActivate();
            var items = await this.App.Items();
            this.Items = new BindableCollection<ItemViewModel>(items);
            var unread = await this.App.UnreadItems();
            this.UnreadItems = new BindableCollection<ItemViewModel>(unread);
            var starred = await this.App.StarredItems();
            this.StarredItems = new BindableCollection<ItemViewModel>(starred);
        }

        public void OnItemSelected(ItemViewModel item)
        {
            //TODO: move navigation code of ShellViewModel to shareable service
            this.Navigation.NavigateToViewModel<ReadItemViewModel>(item);
        }
    }
}