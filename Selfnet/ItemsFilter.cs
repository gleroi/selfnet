using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selfnet
{
    public class ItemsFilter
    {
        public Status ItemStatus { get; set; }

        public string SearchTerm { get; set; }

        public string Tag { get; set; }

        public int? SourceId { get; set; }

        public int? Offset  { get; set; }

        public int? ItemsCount { get; set; }

        public DateTime? UpdatedSince { get; set; }

        public IEnumerable<KeyValuePair<string, string>> AsPairs()
        {
            var parameters = new Dictionary<string, string>();

            if (this.ItemStatus != Status.Any)
            {
                parameters["type"] = this.ItemStatus == Status.Starred ? "starred" : "unread";
            }

            if (!String.IsNullOrWhiteSpace(this.SearchTerm))
            {
                parameters["search"] = this.SearchTerm;
            }

            if (!String.IsNullOrWhiteSpace(this.Tag))
            {
                parameters["tag"] = this.Tag;
            }

            if (this.SourceId.HasValue)
            {
                parameters["source"] = this.SourceId.Value.ToString();
            }

            if (this.Offset.HasValue)
            {
                parameters["offset"] = this.Offset.Value.ToString();
            }

            if (this.ItemsCount.HasValue)
            {
                parameters["items"] = this.ItemsCount.Value.ToString();
            }

            if (this.UpdatedSince.HasValue)
            {
                parameters["updatedsince"] = this.UpdatedSince.Value.ToString("yyyy-MM-dd hh:mm:ss");
            }

            return parameters;
        }
    }
}
