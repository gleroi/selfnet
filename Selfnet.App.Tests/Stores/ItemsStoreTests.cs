using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selfnet.App.Tests.Stores
{
    public class ItemsStoreTests
    {
        public string DbPath { get; set; }

        public ItemsStoreTests()
        {
            this.DbPath = Path.GetTempFileName();
        }

        [Fact]
        public void ItemStore_ShouldPersistSave()
        {
            var items = new List<Item>
            {
                new Item { Id = 1, Title = "titre 1", Content = "contenu 1", Source = 1 },
                new Item { Id = 2, Title = "titre 2", Content = "contenu 2", Source = 1 },
                new Item { Id = 3, Title = "titre 3", Content = "contenu 3", Source = 1 },
            };

            using (var store = new ItemsStore(this.DbPath))
            {
                store.Add(items);
            }

            using (var store = new ItemsStore(this.DbPath))
            {
                var reloadItems = store.All();
                Assert.NotNull(reloadItems);
                Assert.NotEmpty(reloadItems);
            }
        }
    }
}
