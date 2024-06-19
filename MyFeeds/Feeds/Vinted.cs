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
using MyFeeds.Clients;
using Fluid;
using Microsoft.Extensions.FileProviders;
using Microsoft.Azure.Functions.Worker;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace MyFeeds.Feeds
{
    public class Vinted : FeedBuilder
    {
        private readonly VintedClient _vintedClient;

        public Vinted(VintedClient vintedClient, ILoggerFactory loggerFactory ) : base(loggerFactory)
        {
            _vintedClient = vintedClient;
        }

        public override async Task<List<Feed>> GetFeeds()
        {
            string Title = "Vinted";
            string Subtitle = "Rejoins la communauté de mode de seconde main qui compte plus de 65 millions de membres.";
            string _webLink = "https://www.vinted.com";

            Dictionary<string, string> vintedFeedsInputs = new Dictionary<string, string>();
            List<Feed> vintedFeeds = new List<Feed>();

            vintedFeedsInputs.Add("entoile", "search_text=entoil%C3%A9&catalog[]=32&size_ids[]=1594&size_ids[]=1595&size_ids[]=1610&size_ids[]=1611");
            vintedFeedsInputs.Add("sebago-beige", "search_text=&catalog[]=2656&color_ids[]=4&color_ids[]=20&size_ids[]=782&size_ids[]=784&size_ids[]=785&brand_ids[]=20413");

            foreach (KeyValuePair<string, string> item in vintedFeedsInputs)
            {
                Feed feed = new Feed(Title + "-" + item.Key, Subtitle, _webLink+"/"+item.Value);

                List<Article> articles = await GetArticles(item.Value);
                feed.Articles.AddRange(articles);

                vintedFeeds.Add(feed);

            }


            return vintedFeeds;
        }

        private async Task<List<Article>> GetArticles(string searchRoute)
        {
            List<Article> articles = new List<Article>();
            int itemNumber = 15;

            List<ItemSummary> items = await _vintedClient.SearchItems(itemNumber, searchRoute);

            // 20 passe
            // 50 ne passe pas
            foreach (ItemSummary summaryItem in items.Take(itemNumber))
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
                    Content = BuildContent(item),
                    MediaLink = "",
                    Updated = item.UpdatedAtTs.Value,
                    Category = "Clothes",
                    Author = item.User.Login
                };

                articles.Add(article);

            }

            return articles;
        }

        private string BuildContent(Item item)
        {
            item.Description = item.Description.Replace("\n", "<br>");
            TemplateOptions templateOptions = TemplateOptions.Default;
            templateOptions.MemberAccessStrategy = new UnsafeMemberAccessStrategy();

            FluidParser parser = new FluidParser();

            string rootPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string templatePath = Path.Combine(rootPath, "Ressources", "VintedTemplate.html");
            string source = File.ReadAllText(templatePath);

            if (parser.TryParse(source, out IFluidTemplate? fluidTemplate, out string? error))
            {
                TemplateContext context = new TemplateContext(item, templateOptions);

                string renderedValue = fluidTemplate.Render(context);

                return renderedValue;
            }

            return "";
        }
    }
}


