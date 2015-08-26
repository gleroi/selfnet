using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet.App
{
    class ItemsStore
    {
        private readonly ICollection<Item> Items = new List<Item>();

        public ICollection<Item> All()
        {
            return this.Items;
        }

        public void Add(IEnumerable<Item> items)
        {
            foreach (var item in items)
            {
                this.Items.Add(item);
            }
        }
    }
}
