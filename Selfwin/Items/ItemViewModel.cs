using System;
using System.Linq;
using Caliburn.Micro;
using HtmlAgilityPack;
using Selfnet;
using Selfwin.Core;

namespace Selfwin.Items
{
    public class ItemViewModel : PropertyChangedBase, IItemViewModel
    {
        public Item Parameter { get; set; }

        public ItemViewModel(SelfWinSettings settings, Item item)
        {
            this.Parameter = item;
            this.InitFavicon(settings, item.Icon);
            this.InitContent(this.Parameter);
        }

        private void InitFavicon(SelfWinSettings settings, string icon)
        {
            var root = settings.SelfossOptions.Url();
            this.SourceIconUrl = $"{root}/favicons/{icon}";
        }


        private string title;
        public string Title
        {
            get { return this.title ?? (this.title = HtmlEntity.DeEntitize(this.Parameter.Title)); }
        } 

        public string SourceTitle => this.Parameter.SourceTitle;

        public string SourceIcon => this.Parameter.Icon;

        public string SourceIconUrl { get; private set; }

        public string Link => this.Parameter.Link;

        public DateTime PublishedAt => this.Parameter.PublishedAt;

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