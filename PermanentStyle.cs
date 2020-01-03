using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

            XmlSerializer serializer = new XmlSerializer(typeof(Channel));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(feedUrl);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));

                Channel channel = (Channel)serializer.Deserialize(readStream);
                readStream.Close();

                foreach (Item item in channel.Item)
                {
                    // Get the content of the article
                    var web = new HtmlWeb();
                    var doc = web.Load(item.Link2);

                                string xPath = @"//div[1]/div[1]/div/div/[contains(@class, 'siteorigin-widget-tinymce')]";
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xPath);


                    articles.Add(new Article
                    {
                        Id = item.Guid,
                        HTMLTitle = item.Title,
                        Title = item.Title,
                        WebsiteUrl = item.Link2,
                        Link = item.CommentRss,
                        Summary = item.Description,
                        Content = "item.Link2",
                        MediaLink = item.CommentRss,
                        Updated = DateTime.Parse(item.PubDate),
                        Category = item.Category[0]
                    });
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