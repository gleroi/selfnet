using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet.App.Tests.Fakes
{
    class SelfossApiFake : ISelfossApi
    {
        public ICollection<Item> ServerItems = new List<Item>();

        public SelfossApiFake()
        {
            this.Items = new ItemsApiFake(this.ServerItems);
        }

        public IItemsApi Items { get; private set; }
        public ITagsApi Tags { get; private set; }
        public ISourcesApi Sources { get; private set; }

        public Task<bool> Login()
        {
            throw new NotImplementedException();
        }
    }

    internal class ItemsApiFake : IItemsApi {
        ICollection<Item> ServerItems { get; set; }

        public ItemsApiFake(ICollection<Item> serverItems)
        {
            this.ServerItems = serverItems;
        }

        public Task<IEnumerable<Item>> Get()
        {
            return Task.FromResult<IEnumerable<Item>>(this.ServerItems);
        }

        public Task<IEnumerable<Item>> Get(ItemsFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkRead(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkAllRead(params int[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkUnread(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkStarred(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MarkUnstarred(int id)
        {
            throw new NotImplementedException();
        }
    }
}
