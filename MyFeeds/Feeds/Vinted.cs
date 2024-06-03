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

            IEnumerable<string> test = await _vintedClient.SearchItems();

            List<Article> articles = new List<Article>();

            return articles;
        }
    }
}


