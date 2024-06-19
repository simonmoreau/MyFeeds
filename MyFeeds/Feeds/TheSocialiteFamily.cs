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

namespace MyFeeds.Feeds
{
    public class TheSocialiteFamily : FeedBuilder   
    {
        private string _webLink;

        public override async Task<List<Feed>> GetFeeds()
        {
            string Title = "The Socialite Family";
            string Subtitle = "Each week, dive into the captivating worlds of inspiring family interiors.";
            _webLink = "https://www.thesocialitefamily.com";

            Feed feed = new Feed(Title, Subtitle, _webLink);
            List<Article> articles = await GetArticles();
            feed.Articles.AddRange(articles);

            return new List<Feed>() { feed };
        }

        private async Task<List<Article>> GetArticles()
        {
            string mainPageUrl = "https://www.thesocialitefamily.com/fr-fr/media/familles-articles-les-plus-recents";

            // Get the content of the article
            HtmlWeb web = new HtmlWeb();
            web.UserAgent = "Dotnet";

            HtmlDocument doc = await web.LoadFromWebAsync(mainPageUrl);

            string ClassToGet = "styles_card__lwlOE styles_ArticleCard__h9V9N";
            string xPath = @"//a[@class='" + ClassToGet + "']";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            List<Task<Article>> tasksArticles = new List<Task<Article>>();
            foreach (HtmlNode? node in htmlNodes)
            {
                string link = _webLink + node.Attributes["href"].Value;
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

            string ClassToGet = "styles_ArticlePage__h3P0J";
            string xPath = @"//main[@class='" + ClassToGet + "']";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            if (htmlNodes == null) return null;

            string content = htmlNodes.FirstOrDefault()?.InnerHtml;

            if (content == null) return null;

            string jsonArticleId = htmlNodes.FirstOrDefault()?.SelectSingleNode("./script").InnerText;
            ArticleId articleId = JsonSerializer.Deserialize<ArticleId>(jsonArticleId);

            

            return new Article
            {
                Id = articleId.mainEntityOfPage.id,
                HTMLTitle = articleId.headline,
                Title = articleId.headline,
                WebsiteUrl = link,
                Link = link,
                Summary = articleId.headline,
                Content = content,
                MediaLink = "",
                Updated = DateTime.Parse(articleId.datePublished),
                Category = "Lifestyle",
                Author = articleId.author.name
            };


        }
    }

    public class Author
    {
        public string name { get; set; }
    }

    public class MainEntityOfPage
    {
        [JsonPropertyName("@id")]
        public string id { get; set; }
    }

    public class ArticleId
    {
        public MainEntityOfPage mainEntityOfPage { get; set; }
        public string headline { get; set; }
        public List<string> image { get; set; }
        public string datePublished { get; set; }
        public string dateModified { get; set; }
        public Author author { get; set; }
        public Author publisher { get; set; }
    }
}
