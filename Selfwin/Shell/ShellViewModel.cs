using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Selfwin.Items;
using Selfwin.Settings;
using Selfwin.Sources;
using Selfwin.Tags;

namespace Selfwin.Shell
{
    public class ShellViewModel : Screen
    {
        public ShellViewModel(WinRTContainer container)
        {
            this.Container = container;
        }

        public INavigationService Navigation { get; private set; }

        WinRTContainer Container { get; }

        public void OnFrameAvailable(Frame frame)
        {
            this.Navigation = this.Container.RegisterNavigationService(frame);
            this.Navigation.Navigated += this.OnNavigated;
            this.Items();
        }

        private void OnNavigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            this.NotifyOfPropertyChange(nameof(CanBack));
        }

        public void Items()
        {
            this.Navigation.NavigateToViewModel<AllItemsViewModel>();
        }

        public void Tags()
        {
            this.Navigation.NavigateToViewModel<AllTagsViewModel>();
        }

        public void Sources()
        {
            this.Navigation.NavigateToViewModel<AllSourcesViewModel>();
        }

        public void Settings()
        {
            this.Navigation.NavigateToViewModel<SettingsViewModel>();
        }

        public void Back()
        {
            if (this.Navigation != null)
            {
                this.Navigation.GoBack();
            }
        }

        public bool CanBack
        {
            get
            {
                if (this.Navigation != null)
                {
                    return this.Navigation.CanGoBack;
                }
                return false;
            }
        }
    }
}