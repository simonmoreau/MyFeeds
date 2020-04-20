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
    public class PhilippeSilberzahn : WebsiteFeed
    {
        public PhilippeSilberzahn()
        {
            this.Title = "Le blog de Philippe Silberzahn";
            this.Subtitle = "Innovation, entrepreneuriat, surprises stratégiques et ruptures: L'incertitude nous rend libres";
            this.WebLink = "https://philippesilberzahn.com/";
            this.FeedId = "PhilippeSilberzahnFeed";
            this.FeedLink = HTTP2RSS.BlobBaseUrl + this.FeedId + ".xml";
            this.Articles = RebuildFeed();
        }

        private List<Article> RebuildFeed()
        {
            List<Article> articles = new List<Article>();


            string feedUrl = "https://philippesilberzahn.com/feed/";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                // Get the content of the article
                var web = new HtmlWeb();
                var test = web.Load("https://google.com/");

                var doc = web.Load(item.Links.FirstOrDefault().Uri.ToString());
                
                string ClassToGet = "post type-post status-publish format-standard";
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
                    Content = content.Replace("\b", ""),
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