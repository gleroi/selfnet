using System.Linq;
using Xunit;

namespace Selfnet.App.Tests
{
    public class GivenEmptyDatabase_GetItems
    { 
        Context Context { get; set; }

        public GivenEmptyDatabase_GetItems()
        {
            this.Context = new Context();
            this.Context.GivenEmptyDatabase();
        }

        [Fact]
        public void ShouldReturnEmptyList()
        {
            var app = Context.App();

            var items = app.GetItems();

            Assert.NotNull(items);
            Assert.Empty(items);
            var db = Context.ItemsStore();
            var dbItems = db.All();

            Assert.NotNull(dbItems);
            Assert.Equal(0, dbItems.Count);
        }

        [Fact]
        public void ShouldPopulateDbWithServerItems()
        {
            Context.GivenServerHasItems(new Item() {Id = 1});

            var app = Context.App();

            var items = app.GetItems();

            var db = Context.ItemsStore();
            var dbItems = db.All();

            Assert.NotNull(dbItems);
            Assert.Equal(1, dbItems.Count);

            var item = dbItems.First();
            Assert.Equal(1, item.Id);
        }
    }
}
