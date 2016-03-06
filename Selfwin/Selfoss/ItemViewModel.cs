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

        private string title;
        public string Title
        {
            get { return this.title ?? (this.title = HtmlEntity.DeEntitize(this.Parameter.Title)); }
        } 

        public string SourceTitle => this.Parameter.SourceTitle;

        public string SourceIcon => this.Parameter.Icon;

        public bool Unread
        {
            get { return this.Parameter.Unread; }
            set
            {
                this.Parameter.Unread = value;
                this.NotifyOfPropertyChange();
            }
        }

        public bool Starred
        {
            get { return this.Parameter.Starred; }
            set
            {
                this.Parameter.Starred = value;
                this.NotifyOfPropertyChange();
            }
        }

        public string Html
        {
            get { return this.Parameter.Content; }
            set
            {
                if (value == this.Parameter.Content) return;
                this.Parameter.Content = value;
                NotifyOfPropertyChange();
            }
        }

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
                .Take(5)
                .Where(n => !String.IsNullOrWhiteSpace(n.InnerText))
                .Select(n => HtmlEntity.DeEntitize(n.InnerText));
            this.Content = String.Join("\n", text);
        }
    }
}