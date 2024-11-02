using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyFeeds.Feeds
{
    internal class Batirama : FeedBuilder
    {
        public Batirama(ILoggerFactory loggerFactory) : base(loggerFactory)
        {
        }

        public override async Task<List<Feed>> GetFeeds()
        {
            string Title = "Batirama";
            string Subtitle = "Retrouvez l'intégralité de l'actualité de Batirama en temps réel";
            string WebLink = "https://www.batirama.com/";

            Feed feed = new Feed(Title, Subtitle, WebLink);
            List<Article> articles = await GetArticles();
            feed.Articles.AddRange(articles);

            return new List<Feed>() { feed };
        }

        private async Task<List<Article>> GetArticles()
        {
            string feedUrl = "https://www.batirama.com/rss/2-l-info-actualites.html";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            List<Task<Article>> tasksArticles = new List<Task<Article>>();
            foreach (SyndicationItem item in feed.Items)
            {
                tasksArticles.Add(GetArticle(item));
            }

            List<Article> articles = (await Task.WhenAll<Article>(tasksArticles.ToArray())).ToList();

            return articles;
        }

        private static async Task<Article> GetArticle( SyndicationItem item)
        {
            // Get the content of the article
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = await web.LoadFromWebAsync(item.Links.FirstOrDefault().Uri.ToString());

            string ClassToGet = "post post-default post-variant-3";
            string xPath = @"//div[@class='" + ClassToGet + "']";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);
            string content = htmlNodes.FirstOrDefault().InnerHtml;

            return new Article
            {
                Id = item.Id,
                HTMLTitle = item.Title.Text,
                Title = item.Title.Text,
                WebsiteUrl = item.Links.FirstOrDefault().Uri.ToString(),
                Link = item.Links.FirstOrDefault().Uri.ToString(),
                Summary = item.Summary.Text,
                Content = content,
                MediaLink = "",
                Updated = item.PublishDate.UtcDateTime,
                Category = item.Categories.FirstOrDefault()?.ToString(),
                Author = item.Authors.FirstOrDefault()?.Name
            };
        }
    }
}
