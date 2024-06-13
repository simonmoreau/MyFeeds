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
using System.Net.Http;
using System.Net;
using MyFeeds.Clients;

namespace MyFeeds.Feeds
{
    public class Vinted : Feed
    {
        private readonly VintedClient _vintedClient;

        public Vinted(VintedClient vintedClient) : base()
        {
            _vintedClient = vintedClient;
            this.Title = "Vinted";
            this.Subtitle = "Rejoins la communauté de mode de seconde main qui compte plus de 65 millions de membres.";
            this.WebLink = "https://www.vinted.com";
        }

        public override async Task<bool> BuildFeed()
        {
            List<Article> articles = await GetArticles();
            Articles.AddRange(articles);
            return true;
        }

        private async Task<List<Article>> GetArticles()
        {
            List<Article> articles = new List<Article>();

            List<ItemSummary> items = await _vintedClient.SearchItems();

            foreach (ItemSummary summaryItem in items.Take(2))
            {
                ItemDetail detailItem = await _vintedClient.GetItem(summaryItem.Id);
                Item item = detailItem.Item;

                Article article = new Article
                {
                    Id = item.Url,
                    HTMLTitle = item.Title + " " + item.SizeTitle + " " + item.Price,
                    Title = item.Title + " " + item.SizeTitle + " " + item.Price,
                    WebsiteUrl = item.Url,
                    Link = item.Url,
                    Summary = item.Title + " " + item.SizeTitle + " " + item.Price,
                    Content = item.Description,
                    MediaLink = "",
                    Updated = item.UpdatedAtTs.Value,
                    Category = "Clothes",
                    Author = item.User.Login
                };

                articles.Add(article);

            }

            return articles;
        }
    }
}


