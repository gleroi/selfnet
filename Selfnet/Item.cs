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

        /// <summary>
        /// id of the source
        /// </summary>
        public int Source { get; set; }

        /// <summary>
        /// filename of the thumbnail if one was fetched by selfoss
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// filename of the favicon if one was fetched by selfoss
        /// </summary>
        public string Icon { get; set; }

        public string Uid { get; set; }

        /// <summary>
        /// link given by the rss feed
        /// </summary>
        public string Link { get; set; }

        public string SourceTitle { get; set; }
        public string Tags { get; set; }
    }
}