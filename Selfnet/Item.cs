using System;

namespace Selfnet
{
    public class Item
    {
        public int Id { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool Unread { get; set; }
        public bool Starred { get; set; }
        public int Source { get; set; }
        public string Thumbnail { get; set; }
        public string Icon { get; set; }
        public string Uid { get; set; }
        public string Link { get; set; }
        public string SourceTitle { get; set; }
        public string Tags { get; set; }
    }
}