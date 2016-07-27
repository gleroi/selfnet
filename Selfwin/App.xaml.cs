using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Caliburn.Micro;
using NotificationsExtensions.Toasts;
using Selfwin.Items;
using Selfwin.Selfoss;
using Selfwin.Settings;
using Selfwin.Shell;
using Selfwin.Sources;
using Selfwin.Tags;
using Microsoft.HockeyApp;

namespace Selfwin
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            InitializeComponent();
            Microsoft.HockeyApp.HockeyClient.Current.Configure("b5a3bbefabf640059226e33cf17a19f8");
        }

        private WinRTContainer container;

        protected override void Configure()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;

            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.BackgroundColor = (Color) Current.Resources["SystemAccentColor"];
                statusBar.BackgroundOpacity = 1;
                statusBar.ForegroundColor = (Color) Current.Resources["SystemAltHighColor"];
            }

            container = new WinRTContainer();
            container.RegisterWinRTServices();

            container.Singleton<SelfwinApp>();
            container.Singleton<IAppNavigation, SelfwinNavigation>();

            container.Singleton<ShellViewModel>();
            container.PerRequest<AllItemsViewModel>();
            container.PerRequest<ReadItemViewModel>();

            container.PerRequest<AllTagsViewModel>();
            container.PerRequest<AllSourcesViewModel>();
            container.PerRequest<SettingsViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            var instance = container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new Exception("Could not locate any instances.");
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootViewFor<ShellViewModel>();

            RegisterBackgroundTasks();

            //ListenRoamingSettingsChanged();
        }

        private void ListenRoamingSettingsChanged()
        {
            var appData = ApplicationData.Current;
            appData.DataChanged += this.AppDataOnDataChanged;
        }

        private void AppDataOnDataChanged(ApplicationData sender, object args)
        {
            var content = new ToastContent()
            {
                Visual = new ToastVisual()
                {
                    TitleText = new ToastText() { Text = "Selfwin" },
                    BodyTextLine1 = new ToastText() { Text = "Settings have roamed to your device" },
                },
            };
            var toast = new ToastNotification(content.GetXml());
            var notifier = ToastNotificationManager.CreateToastNotifier();
            notifier.Show(toast);
        }

        private const string tileBadgeUpdaterTaskName = "TileBadgeUpdaterTask";
        private const string tileBadgeUpdaterEntryPoint = "Selfwin.BackgroundTasks.TileBadgeUpdater";

        private async void RegisterBackgroundTasks()
        {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == tileBadgeUpdaterTaskName)
                    {
                        task.Value.Unregister(true);
                    }
                }

                var taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = tileBadgeUpdaterTaskName;
                taskBuilder.TaskEntryPoint = tileBadgeUpdaterEntryPoint;
                taskBuilder.SetTrigger(new TimeTrigger(15, false));
                var registration = taskBuilder.Register();
            }
        }
    }
}