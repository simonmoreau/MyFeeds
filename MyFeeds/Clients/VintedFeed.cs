using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFeeds.Clients
{
    public class VintedFeed : Azure.Data.Tables.ITableEntity
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public bool Activated { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
