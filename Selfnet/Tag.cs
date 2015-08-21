using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Selfnet
{
    public class Tag
    {
        [JsonProperty(PropertyName = "Tag")]
        public string Name { get; set; }
        public string Color { get; set; }
        public int UnreadCount { get; set; }
    }
}
