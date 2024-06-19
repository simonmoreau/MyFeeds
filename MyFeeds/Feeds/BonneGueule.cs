using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace MyFeeds.Feeds
{
    public class BonneGueule : FeedBuilder
    {
        private string _webLink;

        public override async Task<List<Feed>> GetFeeds()
        {
            string Title = "Bonne Gueule";
            string Subtitle = "Marque de mode pour homme, nos vêtements sont confectionnés en Europe auprès des plus beaux savoir-faire.";
            _webLink = "https://www.bonnegueule.fr";

            Feed feed = new Feed(Title, Subtitle, _webLink);
            List<Article> articles = await GetArticles();
            feed.Articles.AddRange(articles);

            return new List<Feed>() { feed };
        }

        private async Task<List<Article>> GetArticles()
        {
            string mainPageUrl = "https://www.bonnegueule.fr/nos-derniers-articles/";

            // Get the content of the article
            HtmlWeb web = new HtmlWeb();
            web.UserAgent = "Dotnet";

            HtmlDocument doc = await web.LoadFromWebAsync(mainPageUrl);

            string ClassToGet = "article--content";
            string xPath = @"//div[@class='" + ClassToGet + "']";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            List<Task<Article>> tasksArticles = new List<Task<Article>>();
            foreach (HtmlNode? node in htmlNodes)
            {
                HtmlNode linkNode = node.SelectSingleNode("./a");
                string link = _webLink + linkNode.Attributes["href"].Value;
                tasksArticles.Add(GetArticle(link));
            }

            List<Article> articles = (await Task.WhenAll<Article>(tasksArticles.ToArray())).ToList();

            articles.RemoveAll(item => item == null);

            return articles;
        }

        private static async Task<Article> GetArticle(string link)
        {
            // Get the content of the article
            HtmlWeb web = new HtmlWeb();
            web.UserAgent = "Dotnet";

            HtmlDocument doc = await web.LoadFromWebAsync(link);

            string ClassToGet = "bg-white position-relative";
            string xPath = @"//div[@class='" + ClassToGet + "']";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            if (htmlNodes == null) return null;
            string content = htmlNodes.FirstOrDefault()?.InnerHtml;
            if (content == null) return null;

            string titleXpath = @"//h1[@class='h1 color-primary text-center mt-3 mt-lg-8 xxs12 md8 mx-auto']";
            HtmlNode? titleNode = doc.DocumentNode.SelectNodes(titleXpath)?.FirstOrDefault();
            if (titleNode == null) return null;
            string title = titleNode.InnerText;

            string dateXpath = @"//div[@class='xxs6 text-left']";
            HtmlNode? dateNode = doc.DocumentNode.SelectNodes(dateXpath).FirstOrDefault();
            if (dateNode == null) return null;
            string dateText = dateNode.InnerText.Split("Mis")[0].Replace("Publié le : ", "");
            DateTime date = DateTime.Now;
            CultureInfo ci = new CultureInfo("fr-FR");

            bool tryParse = DateTime.TryParse(dateText, ci, out date);
            if (!tryParse)
            {
                DateTime.TryParseExact(dateText, "dd MMMM yyyy", ci, DateTimeStyles.None, out date);
            }



            string authorXpath = @"//div[@class='xxs6 text-right']";
            HtmlNode? authorNode = doc.DocumentNode.SelectNodes(authorXpath).FirstOrDefault();
            if (authorNode == null) return null;
            string author = authorNode.InnerText;

            return new Article
            {
                Id = link,
                HTMLTitle = title,
                Title = title,
                WebsiteUrl = link,
                Link = link,
                Summary = title,
                Content = content,
                MediaLink = "",
                Updated = date,
                Category = "Lifestyle",
                Author = author
            };


        }
    }
}
