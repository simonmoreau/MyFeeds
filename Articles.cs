using System;
using System.Collections.Generic;

namespace HTTP2RSS
{
    public class Article
    {
        public string Id {get;set;}
        public string HTMLTitle {get; set;}
        public string Title {get;set;}
        public string WebsiteUrl {get; set;}
        public string Summary {get;set;}
        public string Content {get;set;}
        public string Category { get; set; }
        public string MediaLink { get; set; }
        public string Link {get;set;}
        public DateTime Updated {get;set;}
    }

    public class WebsiteFeed
    {
        public string FeedId { get; set; }
        public string WebLink {get;set;}
        public string FeedLink {get;set;}
        public string Title { get; set; }
        public string Subtitle {get;set;}
        public List<Article> Articles {get;set;}

    }
}