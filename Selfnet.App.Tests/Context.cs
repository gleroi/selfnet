using System;
using Selfnet.App.Tests.Fakes;

namespace Selfnet.App.Tests
{
    public class Context
    {
        private ItemsStore itemStore;
        private SelfossApiFake api = new SelfossApiFake();

        public SelfnetApp App()
        {
            return new SelfnetApp(ItemsStore(), Api());
        }

        public ItemsStore ItemsStore()
        {
            if (this.itemStore == null)
            {
                this.GivenEmptyDatabase();
            }
            return this.itemStore;
        }

        ISelfossApi Api()
        {
            return api;
        }

        ConnectionOptions ConnectionOptions()
        {
            return new ConnectionOptions
            {
                Host = "host.com",
                Base = "selfoss",
                Username = "username",
                Password = "password",
            };
        }

        public void GivenEmptyDatabase()
        {
            this.itemStore = new ItemsStore();
        }

        public void GivenServerHasItems(params Item[] items)
        {
            foreach (var item in items)
            {
                this.api.ServerItems.Add(item);
            }
        }
    }
}