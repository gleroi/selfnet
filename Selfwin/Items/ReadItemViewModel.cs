using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Selfwin.Selfoss;

namespace Selfwin.Items
{
    public class ReadItemViewModel : Screen
    {
        public ReadItemViewModel(SelfwinApp app, INavigationService navigation, ItemViewModel item)
        {
            this.App = app;
            this.Navigation = navigation;
            if (item != null)
            {
                Parameter = item;
            }
        }

        public INavigationService Navigation { get; }

        public SelfwinApp App { get; }

        public ItemViewModel Parameter { get; set; }

        public SymbolIcon FavoriteSymbol
        {
            get { return new SymbolIcon(this.Parameter.Starred ? Symbol.UnFavorite : Symbol.Favorite); }
        }

        public void ToggleFavorite()
        {
            if (this.Parameter != null)
            {
                this.App.ChangeFavorite(this.Parameter, !this.Parameter.Starred);
            }
            this.NotifyOfPropertyChange(nameof(FavoriteSymbol));
        }

        public SymbolIcon ReadSymbol
        {
            get { return new SymbolIcon(this.Parameter.Unread ? Symbol.Read : Symbol.Mail); }
        }

        public void ToggleRead()
        {
            if (this.Parameter != null)
            {
                this.App.ChangeUnread(this.Parameter, !this.Parameter.Unread);
            }
            this.NotifyOfPropertyChange(nameof(ReadSymbol));
        }

        public void Close()
        {
            if (this.Navigation != null && this.Navigation.CanGoBack)
            {
                this.Navigation.GoBack();
            }
        }
    }
}