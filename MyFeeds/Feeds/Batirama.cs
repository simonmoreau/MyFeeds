using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MyFeeds.Feeds
{
    internal class Batirama : Feed
    {
        public Batirama() : base()
        {
            Title = "Batirama";
            Subtitle = "Retrouvez l'intégralité de l'actualité de Batirama en temps réel";
            WebLink = "https://www.batirama.com/";
            Articles.AddRange(GetArticles());
        }

        private List<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();

            string feedUrl = "https://www.batirama.com/rss/2-l-info-actualites.html";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                // Get the content of the article
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(item.Links.FirstOrDefault().Uri.ToString());

                string ClassToGet = "post post-default post-variant-3";
                string xPath = @"//div[@class='" + ClassToGet + "']";
                HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);
                string content = htmlNodes.FirstOrDefault().InnerHtml;

                articles.Add(new Article
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
                });
            }

            return articles;
        }
    }
}
