using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet
{
    public class Source
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Spout { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public string Error { get; set; }
        public string Favicon { get; set; }
    }
}
