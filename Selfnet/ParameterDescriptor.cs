using System.Collections.Generic;

namespace Selfnet
{
    public class ParameterDescriptor
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Default { get; set; }
        public bool Required { get; set; }
        public List<string> Validation { get; set; }
    }
}