using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Selfwin.Items;
using Selfwin.Settings;
using Selfwin.Sources;
using Selfwin.Tags;

namespace Selfwin.Shell
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive
    {
        IAppNavigation Navigation { get; }
        WinRTContainer Container { get; }

        public ShellViewModel(WinRTContainer container, IAppNavigation navigation)
        {
            this.Container = container;
            this.Navigation = navigation;
            this.Navigation.Initialize(this);
        }

        protected override void OnActivate()
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += (s, a) =>
            {
                if (Navigation.CanBack)
                {
                    Navigation.Back();
                    a.Handled = true;
                }
            };
            this.AllItems();
        }

        void NavigateTo<T>()
            where T : Screen
        {
            this.Navigation.NavigateTo<T>();
            this.NotifyOfPropertyChange(nameof(CanBack));
        }

        public bool CanBack => this.Navigation.CanBack;

        public void Back() => this.Navigation.Back();

        public void AllItems()
        {
            this.NavigateTo<AllItemsViewModel>();
        }

        public void Tags()
        {
            this.NavigateTo<AllTagsViewModel>();
        }

        public void Sources()
        {
            this.NavigateTo<AllSourcesViewModel>();
        }

        public void Settings()
        {
            this.NavigateTo<SettingsViewModel>();
        }
    }
}