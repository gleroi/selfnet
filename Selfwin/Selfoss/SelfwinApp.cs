﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Selfnet;
using Selfwin.Items;

namespace Selfwin.Selfoss
{
    public class SelfwinApp
    {
        public SelfwinApp()
        {
            this.Api = new SelfossApi(new ConnectionOptions()
            {
                Host = "nostromo.myds.me",
                Base = "selfoss",
                Username = "gleroi",
                Password = "cXVa2I0L",
            });
        }

        private SelfossApi Api { get; }

        private List<ItemViewModel> ItemsCache = null;

        public async Task<IList<ItemViewModel>> Items()
        {
            if (this.ItemsCache == null)
            {
                var items = await this.Api.Items.Get(new ItemsFilter()
                {
                });
                var vms = items.Select(item => new ItemViewModel(item)).ToList();
                this.ItemsCache = vms;
                return vms;
            }
            return this.ItemsCache;
        }

        public async Task<IList<ItemViewModel>> UnreadItems()
        {
            var items = await this.Items();
            return items.Where(i => i.Unread).ToList();
        }

        public async Task<IList<ItemViewModel>> StarredItems()
        {
            var items = await this.Items();
            return items.Where(i => i.Starred).ToList();
        }
    }
}