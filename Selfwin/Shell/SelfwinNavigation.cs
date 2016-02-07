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
    }

    public class SelfwinNavigation : IAppNavigation
    {
        private ShellViewModel Conductor { get; }
        private WinRTContainer Container { get; }

        public SelfwinNavigation(WinRTContainer container, ShellViewModel conductor)
        {
            this.Container = container;
            Conductor = conductor;
        }


        private Stack<Screen> PreviousPages { get; } = new Stack<Screen>();
        public bool CanBack => this.PreviousPages.Count > 0;

        public void NavigateTo<T>()
            where T : Screen
        {
            var vm = Container.GetInstance<T>();
            var previous = Conductor.ActiveItem;
            Conductor.ActivateItem(vm);
            this.PreviousPages.Push(previous);
        }

        public void Back()
        {
            if (this.PreviousPages.Count > 0)
            {
                var previous = this.PreviousPages.Pop();
                Conductor.ActivateItem(previous);
            }
            Conductor.NotifyOfPropertyChange(nameof(CanBack));
        }
    }
}