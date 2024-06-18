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
        private readonly string cookie = "Q0c5ck4yVU1pZGx4R0dKai9WWW1mK2IzUFhFNTc2MmpBRG1TSHEvYWNZNXM5Yk1UaXJyZWc0eVFoYi9Rb3BZWVFLYkZKM2VpeU5NN1lTM2sydVBUM2dGOXZ3MVN1eEc2UEp2cnd2SXllSUY2Ujg4YmdQajNMamN2Sy9FZ3E1RStGeTQxZ2RGQjU4ZldlUnFwMGdFSzVlUCtsMEY3MlNFQ3k3bXFRaVk1WHJraEZwa0NRMjNsNWV0R0dwblBPaEJJL3dxeGdvdnVqY1RVeWRaVFJzcmR3cjJFbFRjOUE5Uit4RU55RVZTb05CUVN2bEp2QnJCektLQ0xhbXdWYUxER2hGU0QrRUt0Unc2b1BPdHlVa2V0VkFLRXRFOWczclpEdEk2ZmNTK3BaSnVTMUlOaTZwZU9wMWQwdkU0eVdBTWF4c0luY2cxR1JjWk1uVW5GVUZvOU1wNUtDcjlrbjRZczI2QmdOZzJyRXc2SURDeVgyOW53WFFxMmVLcDNNUXR0UGF4ai81eGJDank4Uzk4WWdrd1FacWxWbVo0V0lCSE1QMGdkQ2xVQndyd0JaWUNmczdZYXNFNytMczlQTUFkU2w5U1FaS0o3eGp6d01kWlo4VUZvTWpxaUI4dEI2Q3lJZzRPMlBxZVBVb21zRHJaRnM1N1hFazZjRGpRVUFNeDJ2dUZkTEdMcWROelp0SFV5bEROYXBQN3VJTDJmSjVldGZCQTFaU1U2QmVvSmYrWnJLbWYwcU01WjhkazY0TjZheGo2WUFUSml5SzIyQjZWaTdyMVphanBSV1RRV1o1V1o2V3M1RkdaOTMyTFYrd3cvd3BCZW1WdVpUbkpsd2JKKzZZQktjZGM1bWNYUklHQVplS3ZrTGhtZjA1elpET1ZaNUFVRVR5SS80T0VjL001ZWtMb3RxZytvSUxJMlo1MDJKNVpMaGZiQnN5VFAycXgwVUFodVVFRUdaZE92bEtuaDVqVkxIaUZWVERNS0dPdGo4UnRvQlNrYWlBbGJVOEgwVU5FWDBUVElSZUpCQmtLQ3I5V3QrMFF6ZWNxS0pqL2tLclZXZkgwL2lXbVNRRGhmd2dZa1M2TVNaVThneU9hSlZPcEpMRVhuQ2xXZCtNTHRqNDN5SkJVWU8wblQvb0RrdStrMCtpdkk2dmFheDZid2E5MDNVUzZPcllNeEd0VENya0swSDMvQ3lHOVNKck15NEdKcXliMk9jYU5FWnYvNndpK1AwVnBSUGp6SkswV3RUNXJHZkdvd1FQUzNVbHp0OU9hQ0dVU1VKSjJGOUNzMGZPVkVvbytlT1MwenRZOGw1TE1ybXNlNlIzQXB0N0VueHo5eDZEVGtWVHBvMWg4emxQbzc0TEhvcWR6ZHA2dDlsWTF3YVJ3ZkxnN3M1d1FnaU9Pa1duSHZSOE5ZcTZmbFBUa1VxNzEvZ0swdVdDTERlNE43UFQwalVEMTZPWldEdXBKQzduUG8rV3ZQREpiQjN1RjcweUZXRTl6SHFVQzl4UEJvb1VTNnR2WnRBT3kvN1luWFJWN1hiVmZic3lxR3I1UXVsb2FmNTYxbHREMzU4VmtRY0ZyYUM3eUYzY09LdXNUL0kzQThaZTExUDZJTUhXOEtPZm5lYW9ycTdYSXZmMGN3bHc2elNJTnJzQno5NWFFcnRZeHRidVlDZTVRVFpYMnZIYzdkdFM5dWtYMlVYLzZkY0pFc0xPKzVCY01WNU0wTzdwY2dEUVNRVkUyUWJwT2lZQ1FlS1BIN0YwWkJsc3c4WGpMcXN3NlJNQVJsWnR4NkZubytRQkNNOS9pQisrRUtOR3l3Z2FQS20zY25EZVpQcUR3QVM2a1lwVEV6T05VM3FBN0piTXFIUkVQemZCMnhEZGRNVmtmcXk5RnlFdzBBOTRtMloxdVc3OHNyVzg2bkNsdTBkNE0yMzJTbGtDbWV3cVhGVEs0aWNGc2xsdVR2enBWT1lzUXlyTGY1SmY2TVRrTWt4ZUR5dTdHSzkzUmFVQTlQNllpSmtqY2x3ZGwrVzRQSjRNQ3VzL2ZQUklsbXF2WUluN0dOZmkvd0dEU0R1VnRnR0RRc2dweEZ5US8vbkttU0gwWEZhRVhuNHB6UjA5dVlXY05YcTNUYUs5U0pWdDVCVWo0RDluREZiTUlLeUtpNHl5bjVpYy9XTFEwVEo5YXloeDZaMnJrb3lSUHpiamg5YzdsMzVQL1JTVVY1dmJJU1JsQm5McjBMQ2I2RW40ZysvK2w4dzVTaWpNbWJFeGR3NVJwVS93eWZCL0oyMFpmcVVrQkJ4RHhmNWdGMFFMcC9SeTlVTCswVGxoaFlDM1JtSDVkbHl4dXdYSktLNUVCdk5EM1lmN3JxNWw1ZUpjbzMrVk55angzbmFTNVJyMVNXZC82Y3VUYWlnSjJCVm8ya0QrRFVMbnpEaXZOTW1rbDI2M0pMaDBSTWF4R3ZjcXVia2tyYlNnL0p5bzRueVAyM0xlTE82cTl1b1JTVHBxd2lkcjFpSFJEdjdTaC9uSWE1dlhRPS0tZ3hpSXFzYk4zZkM0WlIva2h1NWIzQT09--0052d72d39085e19e7531093ceb35b2afea3423b";

        public VintedClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.vinted.fr/");

            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");
            
            Client = client;
        }

        public async Task<List<ItemSummary>> SearchItems()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "api/v2/catalog/items?page=1&per_page=96&search_text=Inis+Me%C3%A1in&catalog_ids=2050&size_ids=207,208,209&brand_ids=&status_ids=&color_ids=&material_ids=&order=newest_first");
            message.Headers.Add("Cookie", $"_vinted_fr_session={cookie};");

            HttpResponseMessage response = await Client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            ItemRequest itemRequest = await JsonSerializer.DeserializeAsync<ItemRequest>(responseStream);

            return itemRequest.Items;
        }

        public async Task<ItemDetail> GetItem(long itemId)
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, $"api/v2/items/{itemId}");
            message.Headers.Add("Cookie", $"_vinted_fr_session={cookie};");

            HttpResponseMessage response = await Client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            ItemDetail item = await JsonSerializer.DeserializeAsync<ItemDetail>(responseStream);

            return item;
        }

    }
}
