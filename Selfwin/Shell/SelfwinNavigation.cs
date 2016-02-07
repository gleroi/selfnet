using System;
using System.Collections.Generic;
using System.Reflection;
using Caliburn.Micro;

namespace Selfwin.Shell
{
    public interface IAppNavigation
    {
        bool CanBack { get; }

        void NavigateTo<T>() where T : Screen;

        void NavigateTo<T>(object param) where T : Screen;

        void Back();
        void Initialize(ConductorBaseWithActiveItem<Screen> conductor);
    }

    public class SelfwinNavigation : IAppNavigation
    {
        private ConductorBaseWithActiveItem<Screen> Conductor { get; set; }
        private WinRTContainer Container { get; }

        public SelfwinNavigation(WinRTContainer container)
        {
            this.Container = container;
        }


        private Stack<Screen> PreviousPages { get; } = new Stack<Screen>();
        public bool CanBack => this.PreviousPages.Count > 0;

        public void NavigateTo<T>()
            where T : Screen
        {
            this.PushViewModem<T>();

            var vm = this.Container.GetInstance<T>();

            this.InnerNavigateTo(vm);
        }

        private void InnerNavigateTo<T>(T vm) where T : Screen
        {
            this.Conductor.ActivateItem(vm);
            this.Conductor.NotifyOfPropertyChange("CanBack");
        }

        private void PushViewModem<T>() where T : Screen
        {
            var previous = this.Conductor.ActiveItem;
            if (previous != null)
            {
                this.PreviousPages.Push(previous);
            }
        }

        public void NavigateTo<T>(object param) where T : Screen
        {
            this.PushViewModem<T>();

            var vm = this.Container.GetInstance<T>();

            this.TrySetParameter(vm, param);

            this.InnerNavigateTo(vm);
        }

        private void TrySetParameter<T>(T vm, object param)
        {
            var property = typeof(T).GetProperty("Parameter", param.GetType());
            if (property != null)
            {
                property.SetValue(vm, param);
            }
        }

        public void Back()
        {
            if (this.PreviousPages.Count > 0)
            {
                var previous = this.PreviousPages.Pop();
                this.Conductor.ActivateItem(previous);
            }
            this.Conductor.NotifyOfPropertyChange(nameof(CanBack));
        }

        public void Initialize(ConductorBaseWithActiveItem<Screen> conductor)
        {
            this.Conductor = conductor;
        }
    }
}