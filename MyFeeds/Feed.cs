using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyFeeds
{
    internal class Feed
    {

        private void WriteFeed()
        {
            SyndicationFeed feed = new SyndicationFeed("Feed Title", "Feed Description", new Uri("http://Feed/Alternate/Link"), "FeedID", DateTime.Now);

            SyndicationPerson sp = new SyndicationPerson("jesper@contoso.com", "Jesper Aaberg", "http://Jesper/Aaberg");
            feed.Authors.Add(sp);

            feed.Description = new TextSyndicationContent("This is a sample feed");

            feed.Generator = "MyFeeds";
            feed.Id = "FeedID";
            feed.ImageUrl = new Uri("http://server/image.jpg");
            feed.Language = "en-us";
            feed.LastUpdatedTime = DateTime.Now;

            SyndicationLink link = new SyndicationLink(new Uri("http://server/link"), "alternate", "Link Title", "text/html", 1000);
            feed.Links.Add(link);

            TextSyndicationContent textContent = new TextSyndicationContent("Some text content");
            SyndicationItem item = new SyndicationItem("Item Title", textContent, new Uri("http://server/items"), "ItemID", DateTime.Now);

            List<SyndicationItem> items = new List<SyndicationItem>();
            items.Add(item);
            feed.Items = items;


            XmlWriter rssWriter = XmlWriter.Create("rss.xml");
            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(feed);
            rssFormatter.WriteTo(rssWriter);
            rssWriter.Close();
        }
    }
}
