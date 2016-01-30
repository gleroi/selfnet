﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Caliburn.Micro;
using Microsoft.ApplicationInsights;
using Selfwin.Items;
using Selfwin.Selfoss;
using Selfwin.Settings;
using Selfwin.Shell;
using Selfwin.Sources;
using Selfwin.Tags;

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
            WindowsAppInitializer.InitializeAsync(
                WindowsCollectors.Metadata |
                WindowsCollectors.Session);
            InitializeComponent();
        }

        private WinRTContainer container;

        protected override void Configure()
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
            container = new WinRTContainer();
            container.RegisterWinRTServices();
         
            container.Singleton<SelfwinApp>();
               
            //TODO: Register your view models at the container
            container.PerRequest<ShellViewModel>();
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
 			if (args.PreviousExecutionState == ApplicationExecutionState.Running)
                return;
            this.DisplayRootViewFor<ShellViewModel>();
        }
    }
}