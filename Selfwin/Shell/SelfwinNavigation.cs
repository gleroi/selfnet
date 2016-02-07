using System.Collections.Generic;
using Caliburn.Micro;

namespace Selfwin.Shell
{
    public interface IAppNavigation
    {
        bool CanBack { get; }

        void NavigateTo<T>()
            where T : Screen;

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
            var previous = this.Conductor.ActiveItem;
            if (previous != null)
            {
                this.PreviousPages.Push(previous);
            }

            var vm = this.Container.GetInstance<T>();
            this.Conductor.ActivateItem(vm);
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