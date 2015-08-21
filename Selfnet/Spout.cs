using System.Collections.Generic;

namespace Selfnet
{
    public class Spout
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Dictionary<string, ParameterDescriptor> Params { get; set; }
    }
}
