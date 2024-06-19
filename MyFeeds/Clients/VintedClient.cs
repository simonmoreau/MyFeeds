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
        private readonly string cookie = "QWZaSEpheEx6UmZLakJHT016VUMwYmxkMmtDRGt1c1ZqMFFFazVYMFhDMUJpK0hvWGpRU3J0VmRpVXZmaFJhRk9NclA3cGdGNmRVcUI0dEpsQ1hTMUtGYnBVMHAzeWpONlBJMjdvWlhOMW1hMldTMUVlUDkxWHZPaitCbHJGOGNJOEJnaUJTYzNDVEhkcXRoZ1NjUlg2S0VpWHAzM0RyMVdzcE5OVjFWQ3NaRWorZGN1bUV6aldENGJoNWhaWWs3bVgrcTRoSjlQRVNGZUp2VCtTdEI0NFN2ZnhVOTIwa3pBaWx5Y0VWL2xvRHdiZ1ZTSmJqbE9IWVhqblQwWEtjLzhLN2R4RDBEeUFybFFEdFdJMkkvOWp1dW5QU3RaWjBJeStlcTNLSVpyQ2pqM2Zqc2NuY1kycERHYlZLRkFZQmJ5ODgvUE1sUldJZXUrQlF6MTZtUlIwek8yQ2tOS0J4dEFoYjF3YjhJOHFCb09uRUtINGs0RGNtWEdBVWZXdmhUdXZTeUxnLzFxTVFRazhkVVVXa29lL0c0bWdVNUtPVkxMTEhQRWhTYkxlN0szY1RFR0Rqd3lXTkEvZXZWMVpkYm1ZSHEyMllLT2hicjVsN1MzN05saGprMVYyc09LWVN5ejA5Q0VnRzduR3IxSGNrdDRuR3pvSHRWcnEwUmRCbEw4SXUxZXZnZ3p6YlUzVHNpQVJLclZMVFJ1WTZTdGFrc0ZxSU8yLy9idmtzWjh2U29IWkN5UFg0SDI4WlkvV1JmSlplT2FRM25HTjJXMk9TaDExM0o5OE8xM1dFb3Y5STcxQ1Q0U0hENUYwdHdiL0VGalNaZlgrang3cjZVQlVsazZoRnFHam9ZRnM1bzlzNnNiSGovVUVMWEo0WU5GdWNTeHhJNm5LL2JsOEREc2o2dmxGNkV3NDJQbnk3QnJsbENxTVd2TG1QM2I5NDl2SnVsTmZrWTdVb05CRy9mc3lvbG5xMHlmRFlTTWg0YTRrZlFGcFRPclk3dGZ5aFgzQmJJQkRPMjQyY1N4bTFIbjhvQjBadnNvRkRnNkh5Tk5oWDdWK2sxL05CUFcxcDBhMEM5YXF0M1Y5NlBIbDdheDUyZ1NJV042YXNpbTNMRVFIWlp4UVJITTNEN05ZT2RsV0hNU3JDL1RPRGV1a2VZMFhaRzZPUVJmOVFxMWxiZmhLZC9xWmdnUXIyZUNjQXBxRVZFck9NUkxWUWdzNTBzQ0s5S1NVUm9haUs3ZVdaREdpVzJPRmdYbTFXOEM0VXp2UlhzTnMwWGthTkltZFpSTVF6MzA4S3R6a2lyeHZuZEtWMXRwakdEdzRuVzZoWnB3NWJhcFJDcGpTUWQySlJCRTB3T1haNlZ5azJ1MXpKN01CdTZySjFlRUF2RXBXZG5zRVVVQU5ZeGZvTmlPUHB6WC8yRXBjYWJwUFRacmVTVWx6ZVpLYnZSeGxqd3ZoSVZ2WmZMRHhmZGdLQWhxWE1XUmFKNnNGRWtxU1lVMzJEYmFaL2NuNFU1U2VZem9xcGg1WGVadUh5QWhCdy9FQzcwMjlSMzQvcmthZkpQZE1YS1ljb0REYXlPNXluaDVTcmFZM1o3NUpmQXdnaU5qVjFZeDFxTk1pNHRWVTRobmNjcEI2TnNVS3g5QkFnc3pDY0JwTWtSOW9CM1RmMWh2dTExMGFCbFZYY3M1V2N4N0FEYUlHU3k2c2xRd3Uxem1xd3NiNnoreG5VZzA2Z0l2NmVHWnRqM21mQyt4cTZUWXVVeE93UHFuU1VvUitvK3EwQ3FtTnNlaHkzVTZOc2x4TmY5cGwrVnVqL1MvRm5pRjJSeXFMY1F0MWxhNnovbkplZ053MTF0WVZHVGloLy9VemJ0OW8wd21SQ0tmL1UwNEMvNHQ3cUZTeFFtYi8xcFlQbnBqZmdRRTZvemsvR2pmeWJNVGFnbmZ5NzdkMzRIdTVmZHhncmprejZkNThPQVhMbnhkajNBZSt0VzMwa1c2TEJtWnlJa0RtQlJoMjVKY04vR0lFdlhXSk9JRXRhc0RXQm9KWnU4QUxCVTYrM1JMQzZiMVpKSEJkQktNczBYbWNDckxDUUloa3QwZW9rcDV0SmdGS2QwZ3h0UGI1ZWptMTRwMzVCSG1ubDI0STRwalJPZ05kWnRyOEFoNldHZjYyTU56OSt3U2RYeVRCRG5MZWlmaDBTOWlKTVFXOVFaSU55ZEZaTnFwbHFPVFhtempWRThUakNUWXNDbW95WkhJY0o4alBZa2gyQysyQUQxQnRSZFZaWERweGQzb3BaZEdGL09GOWJFR2w3VDFaZnBRMWdSSFdQblRKaWhlQmlJOGJIaHRHUTI5dzJnZ2RLL2ZScm53MmpoR3czUU1iN1BvVnVvU0dLczBMTjYrR3Uva3dkamdFU3dlR2NncUNyK2hFaXMvdElNOUlWQk1rOTlzSWFWSEdicUlETXZCTVJiZWR2T1c0U3J2RmplLzdFekduem1DWGFnSFcvM3hVQzlJSUJMZXJMRDZMdEFOWDF5UFhJPS0tWTZHeDdDTVNONFkwNGxhMXNydUE1UT09--3ef1fc7202db2e26f790f2212eb6b7500361d50e";

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
