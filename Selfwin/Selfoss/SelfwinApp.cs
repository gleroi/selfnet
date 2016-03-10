using Selfnet;
using Selfwin.Core;
using Selfwin.Items;

namespace Selfwin.Selfoss
{
    public class SelfwinApp : Core.SelfwinApp
    {
        public override IItemViewModel CreateItemVm(SelfWinSettings settings, Item item)
        {
            return new ItemViewModel(settings, item);
        }
    }
}