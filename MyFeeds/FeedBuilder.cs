using Azure.Storage.Blobs;
using MyFeeds.Feeds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System;
using System.Configuration;
using Google.Protobuf.Compiler;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MyFeeds
{
    public abstract class FeedBuilder
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        public FeedBuilder(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<FeedConverter>();
            _serviceProvider = serviceProvider;
        }

        public abstract Task<List<Feed>> GetFeeds();

        public async Task WriteFeeds(BlobContainerClient blobContainerClient)
        {
            List<Feed> feeds = new List<Feed>();
            try
            {
                feeds.AddRange(await GetFeeds());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return;
            }

            foreach (Feed feed in feeds)
            {
                try
                {
                    WriteFeed(blobContainerClient, feed);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message, feed.Id);
                    continue;
                }
            }
        }

        private void WriteFeed(BlobContainerClient blobContainerClient, Feed feed)
        {
            SyndicationFeed syndicationFeed = new SyndicationFeed(feed.Title, feed.Subtitle, new Uri(feed.Link), feed.Id, DateTime.Now);

            SyndicationPerson sp = new SyndicationPerson("simon@bim42.com", "Simon Moreau", "");
            syndicationFeed.Authors.Add(sp);

            syndicationFeed.Description = new TextSyndicationContent(feed.Subtitle);

            syndicationFeed.Generator = "MyFeeds";
            syndicationFeed.Id = feed.Id;
            syndicationFeed.ImageUrl = new Uri("http://server/image.jpg");
            syndicationFeed.Language = "en-us";
            syndicationFeed.LastUpdatedTime = DateTime.Now;

            SyndicationLink link = new SyndicationLink(new Uri(feed.Link), "alternate", "Link Title", "text/html", 1000);
            syndicationFeed.Links.Add(link);

            List<SyndicationItem> items = new List<SyndicationItem>();

            foreach (Article article in feed.Articles)
            {
                TextSyndicationContent textContent = new TextSyndicationContent(article.Content, TextSyndicationContentKind.Html);
                SyndicationItem item = new SyndicationItem(
                    article.Title, textContent, new Uri(article.Link), article.Id, article.Updated);

                item.Id = article.Id;
                item.Summary = new TextSyndicationContent(article.Summary, TextSyndicationContentKind.Html);
                item.Categories.Add(new SyndicationCategory(article.Category));
                item.Authors.Add(new SyndicationPerson(article.Author));

                items.Add(item);
            }

            syndicationFeed.Items = items;

            Atom10FeedFormatter rssFormatter = new Atom10FeedFormatter(syndicationFeed);
            MemoryStream output = new MemoryStream();
            using (XmlWriter writer = XmlWriter.Create(output))
            {
                rssFormatter.WriteTo(writer);
                writer.Flush();
                output.Position = 0;

                string fileName = $"{feed.Id}.xml";
                BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

                using (Stream stream = output)
                {
                    blobClient.Upload(stream, overwrite: true);
                }

            }
        }
    }
}
