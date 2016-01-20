using Caliburn.Micro;
using Selfwin.Items;

namespace Selfwin.Shell
{
    public class ShellViewModel : Conductor<Screen>
    {
        public ShellViewModel()
        {
            ShowItems();
        }

        private void ShowItems()
        {
            ActivateItem(new AllItemsViewModel());
        }
    }
}