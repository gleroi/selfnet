using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Selfwin.Selfoss;
using Selfwin.Shell;

namespace Selfwin.Items
{
    public class ReadItemViewModel : Screen
    {
        public ReadItemViewModel(SelfwinApp app, IAppNavigation navigation, ItemViewModel item)
        {
            this.App = app;
            this.Navigation = navigation;
            if (item != null)
            {
                Parameter = item;
            }
        }

        public IAppNavigation Navigation { get; }

        public SelfwinApp App { get; }

        public ItemViewModel Parameter { get; set; }

        protected override void OnActivate()
        {
            base.OnActivate();

            var dataManager = DataTransferManager.GetForCurrentView();
            dataManager.DataRequested += this.OnShareDataRequested;
        }

        protected override void OnDeactivate(bool close)
        {
            var dataManager = DataTransferManager.GetForCurrentView();
            dataManager.DataRequested -= this.OnShareDataRequested;

            base.OnDeactivate(close);
        }

        private void OnShareDataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            if (!String.IsNullOrWhiteSpace(this.Parameter.Link))
            {
                args.Request.Data.SetWebLink(new Uri(this.Parameter.Link));
                args.Request.Data.Properties.Title = Package.Current.DisplayName;
                args.Request.Data.Properties.Description = $"Link to {this.Parameter.Title}";
            }
            else
            {
                args.Request.FailWithDisplayText("This item has no address to share");
            }
        }

        public string Html
        {
            get
            {
                var settings = this.App.Settings();
                var url = settings.SelfossOptions.Url();
                var favicon = this.Parameter.SourceIconUrl;

                return "<div class=\"header\">" +
                       $"<img class=\"favicon\" src=\"{favicon}\" />" +
                       $"<h1 class=\"title\">{this.Parameter.Title}</h1>" +
                       $"<h6 class=\"source-title\">{this.Parameter.SourceTitle}</h6>" +
                       "</div>" +
                       this.Parameter.Html;
            }
        }

        public bool CanShare => this.Parameter != null && !String.IsNullOrWhiteSpace(this.Parameter.Link);

        public void Share()
        {
            try
            {
                DataTransferManager.ShowShareUI();
            }
            catch (Exception ex)
            {
                //TODO: report error to user
            }

        }

        public bool CanOpenBrowser => this.Parameter != null && !String.IsNullOrWhiteSpace(this.Parameter.Link);

        public async void OpenBrowser()
        {
            var url = this.Parameter.Link;
            try
            {
                var uri = new Uri(url);
                await Launcher.LaunchUriAsync(uri);
            }
            catch (Exception ex)
            {
                //TODO: report error to user
            }
        }

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
            if (this.Navigation != null && this.Navigation.CanBack)
            {
                this.Navigation.Back();
            }
        }
    }
}