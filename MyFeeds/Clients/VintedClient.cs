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

        public async Task<IEnumerable<string>> GetPosts()
        {
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "api/v2/catalog/items?page=1&per_page=96&search_text=Inis+Me%C3%A1in&catalog_ids=2050&size_ids=207,208,209&brand_ids=&status_ids=&color_ids=&material_ids=");
            message.Headers.Add("Cookie", "_vinted_fr_session=SlVRSENpeStNTXpJdWtyOTZoL1NCOXp6eFJVbWRpWHZXTnRucCtxb1dmMnVZUkxyVXhPdjhPaFdnOENmUzZuMStGMG1rQlc3aHBzTVBDWDIzZU5pZEVKK3R4c2Yyd2QyR01hQXp3Z3JHWGFJSUJSTzdkREV5N3ROUUN1eDIwQlVaQVEzb3pDQWxQZndFeEZCQlQzcGhxbFRTRDZNT1UrRXdMOEtScHhVYis0SFVtcCtLK1NxU2JLOFh3bytkQTRPdUQwYmUzVThZUTNhZzNpWDhKVXZEWE5pRGF5dm8zbEIvRmdrQ0VkZGpVeC8zL0FiT25laVdYZGRPZE5yd3pZWTRoUjIwVyszME5QTEVQVFlUaEZpNlFoM0t0c1dsUmdpck1LMDk0aE4zK0JKRnpHWjZoZDdDVzRqT2tFUFhVN2dSYmNWRURObVpJZ2Z5TGpSRjl4ODhRRXJpRTBZYWxBdktvcEh6V2FxVmJMVU5paE1iaFZzeTFCVUtvclVwMmNDMkFIalVYcGk1VG4vUTJVTzNnMkFhZ0hnRXVqNUExUjM2UklCWUFXaEgwbmloQnBMTkNQazNmb1RCdWVLVStweW9ZZHp3VmFLQWtJcjd3OUIxclQwVDRzUjJlODZ2TmxEQlVSQkNWaG5RSmc0YUhkZU5jNW9YZTRYK3VvbytVdmx5KzBQc25uemppZ1lQL2lHemFCd21XOHRSQ29aWHhBTXE1Mll3VHBPRUdYWWYzamRNRUVFMC94ZXFuY2J4OGNEK2g5UVMyc0FqdHVIdWZiYmZtK2lXM2Qvd2dXRHpBOEh2Z0pUTWJ4ejdPUFdoL1JvaTdsamRZNWNKTnAxTlhmNzlvWERYblFtOHRBWjJjcVIvR2RTcjFqWU9VTUNSZDdORDEzVXN5c2VjOEl1VkVVeHdLOVdKMzhlVzZVQi9NeGlXQzZRWi9aWHQ3ZUZFNnN0dmZPVXJ3aXJ3OHQ5S05Ca0NJRUZYRmdQNExIWFVGSHZsTWNhS3dtOG5JcGRpQkM0bzJ0UXpWRlZmZ1hiS05JSmdqV25RL29JdWhKV3NwUzk0OXdRWmZtODErME1LY0RnZ0lYNXd0NmVQemxTV2ZvMGtoWjNKcCtieTJOa05OUURJWXNycXp4WUNLbXdjT1RudTRXSXNKUWZXdkdGQUorNjhyaGlMSFNFeStMR2lESFBlRkZEaFpNdkdONnEvbm5kbG0ya1BMV1dzdHdDTkptMk4vWkJ5WndhcEVNeFFhNmJEcW1EckhESXhHc1lscnlCVFphNnZBOU0xUG5ndWZtbU5IUHVQZ0hKN1JyR01RbDVFZW1XakNkTndGTVBKb2RRL3F6RXEwTWIvdDhpU3MxSmRYZTZyMHhYZ0hFdy9CWDNEZWNvYzQ2SHFhZ1RHN3crWUdabHBQVlFVYVpsczFtN2IrLzlManRoS1cwSjZlQ1pyWERyRWJDZXRXTUNwT25JMzIrYTBSTHVKQWtCOWp5QlFoV3BPdHBJN0ZEYWx0cWlKVDZERUN2Zk9uUEdwM2t5MytkSzVvTU4zb0duT1JzWjVSUTdRajR6TlNydXFENlUzQStnVW1HKzFvUlRCbWJSRkp2b05qYWFzWjg1dkNLTGc3L1lLZ0F4Y3hXWWFTa0J6amhNSFhXTThqOEJrQ3o2dlVwODFRMlV0bUFxQi8zUkE5UzdyM1JqcjVhdy81Y2wxejFhSnFYeHQ4dUZOaFJwQitZQWtoYWs0aUlGNGQwT2haOGlZUzAvS1JKZEdaZisvVG9ZalNsSldac0hsTG14ejJWVjZUOExvV3UraVBmc3pRS0JZclVQdE8rbFVJR3hMaFdVL2tXTERPNW94YnNMSEJUekVMUXRNTkZYeS94R3h3YUpkMmxURVJnOUlWQkJES0hmMkdrREhnWXBkRnd2bWgrSDQxMU5QNkt5NWJOWStsY1B0dE4xVHJ4ampVaTNoTTJJOTJsMXE2UjZFVGprWWhEMjRzQ200RnM2eFNZcDFoeUFsQjJENm9GOUQvM2FKRDRRblNESmJmNC9hT0U2anBGalZ5L2ZMSk9jYWVGSFo3RkpWZHBCWXlFOHNGWVJHNFFGTVhmNWpQMXBkdGxrMllEcFhGZFlVTG54Njhkb0pUclZ6Tnl3OGhVa3ExRnUzeTJONDQvY0lRbzVMMG9VOG1WYldlclhsSmhvS3oyczBzbUdrRm5UMTNxU1oxT1RHUi91aW9GbXY5a0F5KytPYXJBK3dBcldzYzBIWUtPcmtEU09kT1V1UFRDaGoxejBTV3lTYSs4UHgrWGI5cnIvNWxMZGluM2RFQ1lCYnFHMW9VYnBDQVVJQ29FdXQxTXJyOXFzNnNJb0xtWldqVVJiemNrSlJJSFl0SFI5ZGpoRzZ3ZG8vbVRqcitiOWVMSU5IYXJZYjZkOEtXWDlFamhUQ25CcE8vU3BUVW1PVDVnVHE3cUFOanFOQkxHaEozbVdkbWVCcGxCZVVUcWtmYzkxb2huTzVlZUh4WG9URllJU01XVnVOajkvbkUwPS0tc0Y5TTBabEx6a2tGUHNCNk9TQ2dMUT09--651356d036fccdf31b66dcc4ac4df7c76082a98f;");

            HttpResponseMessage response = await Client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<string>>(responseStream);
        }
    }
}
