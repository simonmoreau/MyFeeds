using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using HtmlAgilityPack;

namespace HTTP2RSS
{
    public class BonneGueule : WebsiteFeed
    {
        public BonneGueule()
        {
            this.Title = "Bonne Gueule";
            this.Subtitle = "Des vêtements et des Hommes";
            this.WebLink = "https://www.bonnegueule.fr/";
            this.FeedId = "BonneGueuleFeed";
            this.FeedLink = HTTP2RSS.BlobBaseUrl + this.FeedId + ".xml";
            this.Articles = RebuildFeed();
        }

        private List<Article> RebuildFeed()
        {
            List<Article> articles = new List<Article>();

            string feedUrl = "https://www.bonnegueule.fr/feed/";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                // Get the content of the article
                var web = new HtmlWeb();
                var doc = web.Load(item.Links.FirstOrDefault().Uri.ToString());

                string ClassToGet = "entry-content clearfix e-content";
                string xPath = @"//div[@class='" + ClassToGet + "']";
                HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

                if (htmlNodes != null)
                {
                    string content = htmlNodes.FirstOrDefault()?.InnerHtml;

                    if (content != null)
                    {
                        articles.Add(new Article
                        {
                            Id = item.Id,
                            HTMLTitle = item.Title.Text,
                            Title = item.Title.Text,
                            WebsiteUrl = item.Links.FirstOrDefault().Uri.ToString(),
                            Link = item.Links.FirstOrDefault().Uri.ToString(),
                            Summary = item.Summary.Text,
                            Content = content.Replace("\b", ""),
                            MediaLink = "",
                            Updated = item.PublishDate.UtcDateTime,
                            Category = item.Categories.FirstOrDefault()?.ToString(),
                            Author = item.Authors.FirstOrDefault()?.Name
                        });
                    }
                }

            }

            return articles;
        }
    }
}