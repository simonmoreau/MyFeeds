﻿using HtmlAgilityPack;
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
    public class BonneGueule : Feed
    {
        public BonneGueule() : base()
        {
            this.Title = "Bonne Gueule";
            this.Subtitle = "Marque de mode pour homme, nos vêtements sont confectionnés en Europe auprès des plus beaux savoir-faire.";
            this.WebLink = "https://www.bonnegueule.fr";
        }

        public override async Task BuildFeed()
        {
            List<Article> articles = await GetArticles();
            Articles.AddRange(articles);
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
                string link = WebLink + linkNode.Attributes["href"].Value;
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

            string ClassToGet = "articlePage--slices";
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
            string date = dateNode.InnerText.Split("Mis")[0].Replace("Publié le : ", "");

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
                Updated = DateTime.Parse(date),
                Category = "Lifestyle",
                Author = author
            };


        }
    }
}