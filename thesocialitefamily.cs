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
    public class TheSocialiteFamily : WebsiteFeed
    {
        public TheSocialiteFamily()
        {
            this.Title = "The Socialite Family";
            this.Subtitle = "Découvrez les intérieurs inspirants de familles contemporaines. Un mélange d&#039;univers design et déco très personnels et étonnants.";
            this.WebLink = "https://www.thesocialitefamily.com";
            this.FeedId = "TheSocialiteFamilyFeed";
            this.FeedLink = HTTP2RSS.BlobBaseUrl + this.FeedId + ".xml";
            this.Articles = RebuildFeed();
        }

        private List<Article> RebuildFeed()
        {
            List<Article> articles = new List<Article>();


            string feedUrl = "https://www.thesocialitefamily.com/feed/";

            XmlReader reader = XmlReader.Create(feedUrl);
            SyndicationFeed feed = SyndicationFeed.Load(reader);
            reader.Close();

            foreach (SyndicationItem item in feed.Items)
            {
                // Get the content of the article
                var web = new HtmlWeb();

                var doc = web.Load(item.Links.FirstOrDefault().Uri.ToString());

                string ClassToGet = "content-strate";
                string xPath = @"//div[@class='" + ClassToGet + "']";
                HtmlNodeCollection contentNodes = doc.DocumentNode.SelectNodes(xPath);

                if (contentNodes != null)
                {
                    HtmlNode contentNode = contentNodes.FirstOrDefault();
                    // Find image nodes
                    string imageClassInContent = "block-strate imgvideo-container";
                    string xPathImageClass = @"//div[@class='" + imageClassInContent + "']";
                    HtmlNodeCollection imageNodes = contentNode.SelectNodes(xPathImageClass);

                    if (imageNodes != null)
                    {
                        // Replace them
                        foreach (HtmlNode imageNode in imageNodes.ToList())
                        {
                            // Find the image link
                            HtmlNode imageSourceNode = imageNode.SelectNodes(@".//img")?.FirstOrDefault();
                            if (imageSourceNode != null)
                            {
                                imageNode.ParentNode.ReplaceChild(imageSourceNode, imageNode);
                            }
                        }
                    }


                    string content = contentNode.InnerHtml;


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

            return articles;
        }
    }
}