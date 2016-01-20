using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Caliburn.Micro;
using HtmlAgilityPack;
using Selfnet;

namespace Selfwin.Items
{
    public class ItemViewModel : PropertyChangedBase
    {

        public ItemViewModel(Item item)
        {
            Item = item;
            this.InitShortDescription(item);
        }

        private Item Item { get; }

        public string Title => this.Item.Title;
        public string SourceTitle => this.Item.SourceTitle;
        public bool Unread => this.Item.Unread;
        public bool Starred => this.Item.Starred;

        private string _shortDescription;
        public string ShortDescription
        {
            get { return _shortDescription; }
            private set
            {
                if (value == _shortDescription) return;
                _shortDescription = value;
                NotifyOfPropertyChange();
            }
        }

        private void InitShortDescription(Item item)
        {
            var elt = new HtmlDocument();
            elt.LoadHtml(item.Content);

            var text = elt.DocumentNode.Descendants("p")
                .Where(n => !String.IsNullOrWhiteSpace(n.InnerText))
                .Select(n => n.InnerText);
            this.ShortDescription = String.Join("\n", text);
        }
    }
}