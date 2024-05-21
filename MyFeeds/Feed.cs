using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyFeeds
{
    internal abstract class Feed
    {
        internal readonly List<Article> Articles = new List<Article>();
        
        public string WebLink { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }


        public readonly string FeedId;
        public readonly string FeedLink;
        internal Feed()
        {
            FeedId = GetType().Name;
            FeedLink = "https://rnbsshrb6gtyyazfunctions.blob.core.windows.net/licences/" + this.FeedId + ".xml";
        }

        public abstract Task BuildFeed();

        public MemoryStream WriteFeed()
        {

            SyndicationFeed feed = new SyndicationFeed(Title, Subtitle, new Uri(FeedLink), FeedId, DateTime.Now);

            SyndicationPerson sp = new SyndicationPerson("simon@bim42.com", "Simon Moreau","");
            feed.Authors.Add(sp);

            feed.Description = new TextSyndicationContent(Subtitle);

            feed.Generator = "MyFeeds";
            feed.Id = FeedId;
            feed.ImageUrl = new Uri("http://server/image.jpg");
            feed.Language = "en-us";
            feed.LastUpdatedTime = DateTime.Now;

            SyndicationLink link = new SyndicationLink(new Uri(FeedLink), "alternate", "Link Title", "text/html", 1000);
            feed.Links.Add(link);

            List<SyndicationItem> items = new List<SyndicationItem>();

            foreach (Article article in Articles)
            {
                TextSyndicationContent textContent = new TextSyndicationContent(article.Content);
                SyndicationItem item = new SyndicationItem(
                    article.Title, textContent, new Uri(article.Link), article.Id, article.Updated);

                item.Id = article.Id;
                item.Summary = new TextSyndicationContent(article.Summary);
                item.Categories.Add(new SyndicationCategory(article.Category));
                item.Authors.Add(new SyndicationPerson(article.Author));

                items.Add(item);
            }

            feed.Items = items;

            Rss20FeedFormatter rssFormatter = new Rss20FeedFormatter(feed, false);
            MemoryStream output = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                rssFormatter.WriteTo(writer);
                writer.Flush();
                output.Position = 0;
                return output;
            }

        }
    }
}
