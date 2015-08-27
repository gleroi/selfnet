using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selfnet.App
{
    public class SelfnetApp
    {
        private readonly ItemsStore Items;
        private readonly ISelfossApi Server;

        public SelfnetApp(ItemsStore items, ISelfossApi server)
        {
            this.Items = items;
            this.Server = server;
        }

        public ICollection<Item> GetItems()
        {
            var items = this.Items.All();
            if (!items.Any())
            {
                this.Refresh();
            }
            return items;
        }

        private async void Refresh()
        {
            var items = await this.Server.Items.Get();
            this.Items.Add(items);
        }
    }
}
