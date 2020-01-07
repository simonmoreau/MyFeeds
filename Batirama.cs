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
    public class Batirama : WebsiteFeed
    {
        public Batirama()
        {
            this.Title = "Batirama";
            this.Subtitle = "Retrouvez l'intégralité de l'actualité de Batirama en temps réel";
            this.WebLink = "https://www.batirama.com/";
            this.FeedId = "BatiramaRSS";
            this.FeedLink = HTTP2RSS.BlobBaseUrl + this.FeedId + ".xml";
            this.Articles = RebuildFeed();
        }

        private List<Article> RebuildFeed()
        {
            List<Article> articles = new List<Article>();

            string feedUrl = "https://www.batirama.com/rss/2-l-info-actualites.html";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                                    // Get the content of the article
                    var web = new HtmlWeb();
                    var doc = web.Load(item.Links.FirstOrDefault().Uri.ToString());

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
                        Category = item.Categories.FirstOrDefault().ToString(),
                        Author = "Simon Crompton"
                    });
            }

            return articles;
        }
    }
}