using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet.App
{
    class ItemsStore
    {
        public ICollection<Item> All()
        {
            return new List<Item>();
        }
    }
}
