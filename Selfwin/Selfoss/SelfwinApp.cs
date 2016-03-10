using Selfnet;
using Selfwin.Core;

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