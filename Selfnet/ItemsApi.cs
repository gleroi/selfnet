﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet
{
    public class ItemsApi : BaseApi
    {
        internal ItemsApi(ConnectionOptions opts, IHttpGateway http)
            : base(opts, http) {}

        public async Task<IEnumerable<Item>> Get()
        {
            var url = BuildUrl("items");
            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            var result = json.ToObject<List<Item>>();
            return result;
        }

        public async Task<IEnumerable<Item>> Get(ItemsFilter filter)
        {
            var parameters = filter.AsPairs();
            var query = String.Join("&", parameters.Select(pair => pair.Key + "=" + pair.Value));
            var url = BuildUrl("items", query);

            var json = await this.Http.Get(url.Uri.AbsoluteUri);
            var result = json.ToObject<List<Item>>();
            return result;
        }

        public async Task<bool> MarkRead(int id)
        {
            var url = BuildUrl("mark/" + id);
            var json = await this.Http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkAllRead(params int[] ids)
        {
            var url = BuildUrl("mark");
            var parameters = ids.Select(id => new KeyValuePair<string, string>("ids[]", id.ToString())).ToList();
            var json = await this.Http.Post(url.Uri.AbsoluteUri, parameters);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkUnread(int id)
        {
            var url = BuildUrl("unmark/" + id);
            var json = await this.Http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkStarred(int id)
        {
            var url = BuildUrl("starr/" + id);
            var json = await this.Http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }

        public async Task<bool> MarkUnstarred(int id)
        {
            var url = BuildUrl("unstarr/" + id);
            var json = await this.Http.Post(url.Uri.AbsoluteUri);
            return this.ReadSuccess(json);
        }
    }
}
