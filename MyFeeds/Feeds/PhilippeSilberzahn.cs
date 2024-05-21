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
    internal class PhilippeSilberzahn : Feed
    {
        public PhilippeSilberzahn() : base()
        {
            this.Title = "Le blog de Philippe Silberzahn";
            this.Subtitle = "Innovation, entrepreneuriat, surprises stratégiques et ruptures: L'incertitude nous rend libres";
            this.WebLink = "https://philippesilberzahn.com/";
        }

        public override async Task BuildFeed()
        {
            List<Article> articles = await GetArticles();
            Articles.AddRange(articles);
        }

        private async Task<List<Article>> GetArticles()
        {
            string feedUrl = "https://philippesilberzahn.com/feed/";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            List<Task<Article>> tasksArticles = new List<Task<Article>>();
            foreach (SyndicationItem item in feed.Items)
            {
                tasksArticles.Add(GetArticle(item));
            }

            List<Article> articles = (await Task.WhenAll<Article>(tasksArticles.ToArray())).ToList();

            articles.RemoveAll(item => item == null);

            return articles;
        }

        private static async Task<Article> GetArticle( SyndicationItem item)
        {
            // Get the content of the article
            HtmlWeb web = new HtmlWeb();
            web.UserAgent = "Dotnet";
            
            HtmlDocument doc = await web.LoadFromWebAsync(item.Links.FirstOrDefault().Uri.ToString());

            string ClassToGet = "entry-content";
            string xPath = @"//div[@class='" + ClassToGet + "']";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            if (htmlNodes == null) return null;
            {
                string content = htmlNodes.FirstOrDefault()?.InnerHtml;

                if (content == null) return null;

                return new Article
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
                };
            }


        }
    }
}
