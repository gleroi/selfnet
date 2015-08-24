using System.Collections.Generic;

namespace Selfnet
{
    public class Source
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<string> Tags { get; set; }
        public string Spout { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public string Error { get; set; }
        public string Favicon { get; set; }
    }
}
