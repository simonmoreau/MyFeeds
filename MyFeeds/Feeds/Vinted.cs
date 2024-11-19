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
using MyFeeds.Utilities;

namespace MyFeeds.Feeds
{
    public class Vinted : FeedBuilder
    {
        private readonly VintedClient _vintedClient;
        private readonly ICycleManager _cycleManager;
        private readonly IVintedFeedRepository _vintedFeedRepository;

        public Vinted(VintedClient vintedClient, ILoggerFactory loggerFactory, ICycleManager cycleManager, IVintedFeedRepository vintedFeedRepository) : base(loggerFactory)
        {
            _vintedClient = vintedClient;
            _cycleManager = cycleManager;
            _vintedFeedRepository = vintedFeedRepository;
        }

        public override async Task<List<Feed>> GetFeeds()
        {
            string Title = "Vinted";
            string Subtitle = "Rejoins la communauté de mode de seconde main qui compte plus de 65 millions de membres.";
            string _webLink = "https://www.vinted.com";

            List<VintedFeed> vintedInputFeeds = await _vintedFeedRepository.GetFeedInputs();
            List<Feed> vintedFeeds = new List<Feed>();

            int i = 0;
            foreach (VintedFeed inputFeed in vintedInputFeeds)
            {
                if (!_cycleManager.CanRun(i, vintedInputFeeds.Count))
                {
                    i++;
                    continue;
                }
                Feed feed = new Feed(Title + "-" + inputFeed.Name, Subtitle, _webLink + "/" + inputFeed.Url);

                List<Article> articles = await GetArticles(inputFeed.Url);
                feed.Articles.AddRange(articles);

                vintedFeeds.Add(feed);
                i++;
            }

            return vintedFeeds;
        }

        private async Task<List<Article>> GetArticles(string searchRoute)
        {
            List<Article> articles = new List<Article>();
            int itemNumber = 20;

            List<ItemSummary> items = await _vintedClient.SearchItems(itemNumber, searchRoute);

            // 20 passe
            // 50 ne passe pas
            foreach (ItemSummary summaryItem in items.Take(itemNumber))
            {
                ItemDetail detailItem = await _vintedClient.GetItem(summaryItem.Id);
                Item item = detailItem.Item;
                string title = $"{item.Title} - {item.TotalItemPrice} {item.Currency} - {item.Size}";

                Article article = new Article
                {
                    Id = item.Url,
                    HTMLTitle = title,
                    Title = title,
                    WebsiteUrl = item.Url,
                    Link = item.Url,
                    Summary = title,
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

        public string BuildContent(Item item)
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


