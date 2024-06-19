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
    public class PermanentStyle : FeedBuilder
    {
        public PermanentStyle(ILoggerFactory loggerFactory, IServiceProvider serviceProvider) : base(loggerFactory, serviceProvider)
        {
        }

        public override async Task<List<Feed>> GetFeeds()
        {
            string Title = "Permanent Style";
            string Subtitle = "The leading British blog on tailoring, luxury and men's style";
            string WebLink = "https://www.permanentstyle.com/";

            Feed feed = new Feed(Title, Subtitle, WebLink);
            List<Article> articles = await GetArticles();
            feed.Articles.AddRange(articles);

            return new List<Feed>() { feed };
        }

        private async Task<List<Article>> GetArticles()
        {
            string feedUrl = "https://www.permanentstyle.com/feed";

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
            HtmlDocument doc = await web.LoadFromWebAsync(item.Links.FirstOrDefault().Uri.ToString());

            string ClassToGet = "siteorigin-widget-tinymce textwidget";
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
                    Content = content,
                    MediaLink = "",
                    Updated = item.PublishDate.UtcDateTime,
                    Category = item.Categories.FirstOrDefault().ToString(),
                    Author = "Simon Crompton"
                };
            }


        }
    }
}
