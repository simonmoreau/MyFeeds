using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFeeds
{
    public class Article
    {
        public string Id { get; set; }
        public string HTMLTitle { get; set; }
        public string Title { get; set; }
        public string WebsiteUrl { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string MediaLink { get; set; }
        public string Link { get; set; }
        public DateTime Updated { get; set; }
        public string Author { get; set; }
    }
}
