using Caliburn.Micro;
using Selfwin.Selfoss;

namespace Selfwin.Items
{
    public class ReadItemViewModel : Screen
    {
        public ReadItemViewModel(SelfwinApp app, ItemViewModel item)
        {
            App = app;
            if (item != null)
            {
                Parameter = item;
            }
        }

        public SelfwinApp App { get; }

        public ItemViewModel Parameter { get; set; }
    }
}