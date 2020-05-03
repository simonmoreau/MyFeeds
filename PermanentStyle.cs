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
    public class PermanentStyle : WebsiteFeed
    {
        public PermanentStyle()
        {
            this.Title = "Permanent Style";
            this.Subtitle = "The leading British blog on tailoring, luxury and men's style";
            this.WebLink = "https://www.permanentstyle.com/";
            this.FeedId = "PermanentStyleRSS";
            this.FeedLink = HTTP2RSS.BlobBaseUrl + this.FeedId + ".xml";
            this.Articles = RebuildFeed();
        }

        private List<Article> RebuildFeed()
        {
            List<Article> articles = new List<Article>();

            string feedUrl = "https://www.permanentstyle.com/feed";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                // Get the content of the article
                var web = new HtmlWeb();
                var doc = web.Load(item.Links.FirstOrDefault().Uri.ToString());

                string ClassToGet = "siteorigin-widget-tinymce textwidget";
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
                            Content = content,
                            MediaLink = "",
                            Updated = item.PublishDate.UtcDateTime,
                            Category = item.Categories.FirstOrDefault().ToString(),
                            Author = "Simon Crompton"
                        });
                    }
                }


            }

            return articles;
        }

        private List<Article> LoopOnAllPages()
        {
            List<Article> articles = new List<Article>();

            string[] categories = new string[] { "cravates", "pochettes", "costumes", "vestes", "manteaux", "chemises", "pantalons", "souliers", "pulls", "smokings-vetement-formel", "maroquinerie", "accessoires", "le-coin-des-dames" };

            foreach (string category in categories)
            {
                string url = $"https://vestiairedurenard.fr/produits/{category}/";
                articles.AddRange(
                    ParseHTMLForArticles(category, url)
                );
            }

            articles = articles.OrderByDescending(a => a.Updated).ToList();
            return articles;
        }

        private List<Article> ParseHTMLForArticles(string category, string url)
        {
            // From Web
            var web = new HtmlWeb();
            var doc = web.Load(url);

            // class product
            // 
            string xPath = @"//div[3]/div[1]/div[contains(@class, 'type-product')]";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);

            int number = htmlNodes.Count;
            List<Article> articles = new List<Article>();

            foreach (HtmlNode htmlNode in htmlNodes)
            {
                string link = htmlNode.SelectNodes("div[1]/a")[0].GetAttributeValue("href", "");
                string dateText = htmlNode.SelectNodes("div[2]/div[1]/span")[0].InnerText.Trim();
                dateText = dateText.Replace("th", "").Replace("nd", "").Replace("st", "").Replace("rd", "");
                DateTime date = DateTime.ParseExact(dateText, "MMMM d\\, yyyy", System.Globalization.CultureInfo.GetCultureInfo("fr-FR"));

                string price = htmlNode.SelectNodes("div[3]/span")[0].InnerText.Replace("&nbsp;&", " ");
                string imgHtml = $"<img src=\"{htmlNode.SelectNodes("div[1]/a")[0].GetAttributeValue("id", "")}\" > <br />";
                string content = imgHtml + htmlNode.SelectNodes("div[2]")[0].InnerHtml;
                articles.Add(new Article
                {
                    Id = link,
                    HTMLTitle = htmlNode.SelectNodes("h3")[0].InnerHtml + " - " + price,
                    Title = htmlNode.SelectNodes("h3")[0].InnerText + " - " + price,
                    WebsiteUrl = url,
                    Link = link,
                    Summary = price + " - " + htmlNode.SelectNodes("h3")[0].InnerText,
                    Content = content,
                    MediaLink = htmlNode.SelectNodes("div[1]/a")[0].GetAttributeValue("id", ""),
                    Updated = date,
                    Category = category
                });
            }

            return articles;

        }
    }
}