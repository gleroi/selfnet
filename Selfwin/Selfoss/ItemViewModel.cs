﻿using System;
using System.Linq;
using Caliburn.Micro;
using HtmlAgilityPack;
using Selfnet;

namespace Selfwin.Selfoss
{
    public class ItemViewModel : PropertyChangedBase
    {
        public Item Parameter { get; set; }

        public ItemViewModel(Item item)
        {
            this.Parameter = item;
            this.InitContent(this.Parameter);
        }

        public string Title => this.Parameter.Title;
        public string SourceTitle => this.Parameter.SourceTitle;
        public bool Unread => this.Parameter.Unread;
        public bool Starred => this.Parameter.Starred;

        private string _content;
        public string Content
        {
            get { return _content; }
            private set
            {
                if (value == _content) return;
                _content = value;
                NotifyOfPropertyChange();
            }
        }

        private void InitContent(Item item)
        {
            var elt = new HtmlDocument();
            elt.LoadHtml(item.Content);

            var text = elt.DocumentNode.Descendants("p")
                .Where(n => !String.IsNullOrWhiteSpace(n.InnerText))
                .Select(n => n.InnerText);
            this.Content = String.Join("\n", text);
        }
    }
}