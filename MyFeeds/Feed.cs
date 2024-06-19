using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyFeeds
{
    public class Feed
    {
        public readonly List<Article> Articles = new List<Article>();
        public readonly string Title;
        public readonly string Subtitle;
        public readonly string WebLink;
        public readonly string Id;
        public readonly string Link;
        public Feed(string title, string subtitle, string webLink)
        {
            Title = title;
            Subtitle = subtitle;
            WebLink = webLink;
            Id = GetType().Name;
            Link = "https://rnbsshrb6gtyyazfunctions.blob.core.windows.net/licences/" + this.Id + ".xml";
        }

    }
}
