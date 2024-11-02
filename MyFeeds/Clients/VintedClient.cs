using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyFeeds.Clients
{
    public class VintedClient
    {
        public HttpClient Client { get; }

        public VintedClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.vinted.fr/");

            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");
            
            Client = client;
        }

        public async Task<List<ItemSummary>> SearchItems(int itemNumber, string searchRoute)
        {
            string baseRoute = $"api/v2/catalog/items?page=1&per_page={itemNumber.ToString()}";
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"{baseRoute}&{searchRoute}&order=newest_first");

            HttpResponseMessage response = await Client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            ItemRequest itemRequest = await JsonSerializer.DeserializeAsync<ItemRequest>(responseStream);

            return itemRequest.Items;
        }

        public async Task<ItemDetail> GetItem(long itemId)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"api/v2/items/{itemId}");

            HttpResponseMessage response = await Client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            ItemDetail item = await JsonSerializer.DeserializeAsync<ItemDetail>(responseStream);

            return item;
        }

    }
}
