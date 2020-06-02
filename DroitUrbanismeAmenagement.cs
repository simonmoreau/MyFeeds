using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HtmlAgilityPack;

namespace HTTP2RSS
{
    public class DroitUrbanismeAmenagement : WebsiteFeed
    {
        public DroitUrbanismeAmenagement()
        {
            this.Title = "Le blog du droit de l'urbanisme et de l'aménagement";
            this.Subtitle = "Le blog du droit de l'urbanisme et de l'aménagement";
            this.WebLink = "https://droit-urbanisme-et-amenagement.efe.fr/";
            this.FeedId = "DroitUrbanismeAmenagementRSS";
            this.FeedLink = HTTP2RSS.BlobBaseUrl + this.FeedId + ".xml";
            this.Articles = ListArticleOnThePage();
        }

        private List<Article> ListArticleOnThePage()
        {
            List<Article> articles = new List<Article>();

            string blogUrl = "https://droit-urbanisme-et-amenagement.efe.fr/";
            var web = new HtmlWeb();
            var doc = web.Load(blogUrl);

            string xPath = @"//h2/a";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            if (htmlNodes != null)
            {
                foreach (HtmlNode htmlNode in htmlNodes)
                {
                    string link = htmlNode.GetAttributeValue("href", "");

                    if (!string.IsNullOrEmpty(link))
                    {
                        articles.Add(ParseHTMLForArticles(link));
                    }
                }
            }

            articles = articles.OrderByDescending(a => a.Updated).ToList();
            return articles;
        }

        private Article ParseHTMLForArticles(string url)
        {
            // From Web
            var web = new HtmlWeb();
            var doc = web.Load(url);

            string xPath = @"//div[contains(@class, 'entry-content')]";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            HtmlNode htmlNode = htmlNodes.FirstOrDefault();

            string content = "";
            string summary = "";
            if (htmlNode != null)
            {

                content = htmlNode.InnerHtml;
                if (htmlNode.InnerText.Length > 150)
                {
                    summary = htmlNode.InnerText.Substring(0,150);
                }
                else
                {
                    summary = htmlNode.InnerText;
                }
            }

            string titlexPath = @"//h1[contains(@class, 'entry-title')]";
            HtmlNodeCollection titleHtmlNodes = doc.DocumentNode.SelectNodes(titlexPath);

            HtmlNode titleHtmlNode = titleHtmlNodes.FirstOrDefault();

            string title = "";
            if (titleHtmlNode != null)
            {
                title = titleHtmlNode.InnerText;
            }

            string datexPath = @"//span[contains(@class, 'updated')]";
            HtmlNodeCollection dateHtmlNodes = doc.DocumentNode.SelectNodes(datexPath);

            HtmlNode dateHtmlNode = dateHtmlNodes.FirstOrDefault();

            DateTime date = DateTime.Now;
            if (dateHtmlNode != null)
            {
                CultureInfo provider = new CultureInfo("fr-FR"); 
                DateTime.TryParse(dateHtmlNode.InnerText, provider, DateTimeStyles.None, out date);
            }

            return new Article
            {
                Id = url,
                HTMLTitle = title,
                Title = title,
                WebsiteUrl = url,
                Link = url,
                Summary = summary,
                Content = content,
                MediaLink = "",
                Updated = date,
                Category = "Droit de l'Urbanisme"
            };
        }
    }
}