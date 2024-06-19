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
        private readonly string cookie = "MGtPU2o0ZDFrU1hMMGhxTXYrS3FoSElaLzkzN1V0TEdPU2V2NkwrUFlUS0NDVUVCRTd1V0xQUEdRMm9GYVdWR3NrOWsyYzNJV0duOUNFSU13anV0WWNCZTA1dFhVVnJzYVFKVEJsbG9MMFV4WkJwMnkwbWdFTFpyQ3JlN25UbE15VU12dUVudTRxVllTeVloNFdiNytzakFqdlRaZUNWcktsMHl5SnhVeFNnYVlVc0NXbmhYdkV2K09GZlQwZlV5RExJMGN4VThadXNzVmY5cWVyYjBGNXE3aldTUFJ2M05SbitMeENHRVlHVUY1cU42VmF1OExya2JURWxXbjdHaDV4aTQ5V1NJVk0yVTB4SzZUcS9SQlB6ZTMySjRFdGRZOE1qbVV5WGZKMW9VZ2oydG13eFlmWXJJNlQyMUFIT1lTSlcxdlF4VWVFSHo3MFMzZU1sS1lsekhOZTFvU1F1bVV4OXZmajNEZG83UEtLWXdMMjdiWUlpaWJDclgrNFpZZUU0MXNzTkNyM09iWmQyVmpZYUFaS2RUN1dpMjZmQm80TklKL2k0UndCdy8rUlRrVUhnbC9TbEJHcWFBRDQzdmszNWJxRVZnK0JNV2NIOUR0WWZWWmtCamN2N1NCSzRaNTVBVGJUMkpmQ3J2blk3MUVvSjlqZmJjaXFLS0lFT3JXVHRBVDU3SzNYWHNYQWxra1o0dHIxUWU1UmlzbzEyK3R3MTdNaGlMUkVYZG54K3I2a281Qk1Ga1gzTFEwaStFSTYvS0VzcE1tMUl1T0ladncvTkRNV3V0cjRxWlZGSm40UnExMXhqdk5HT3oyZEhZYnRaWk8ybFhlVENBR1JIdWdxNXFUNW82OXpWYW9VQnhlVEZrd1dmVjRpZTZoZzdjdjlYMFgzV3pWVVVWS0hFclFKVUlXTlZJblBvU2QvNkR5alp4N0hwVFhUNUtUUmtoWEFmVDRlalVuT0JDb1BST0U1NjNoWjQzSmZTWUJ0WE8ra3hUd2pGQU1lS3lNMUxNQnhhUEVScUN2MjE0VkJGWnZqNFRzVjhvYlRhWjFqYWt4UkFuY0NpYzZPY3Y3ckZlZk1ONEg4R1E2N29XbWlFYk02TTJoeEtmVUdwSjZvYXFCZ2NhajZ2TWlacWNiUFZSY21HM0FabU0ycmEyUXo2VkFxM28yaGtiSU5ucDFoaG5zRlJPYTNkYlNGZzR6M1h0aDgzQzhaSEs1WWlpaEJ1c1ZWWXJVc0I4dEo2RGwzNFFXVXJaZkUzazh3TzJBazdKN3VzdG1sdnY1dlFQOHF2bjJxNzFCeWtuc0hvNDFyRUhxZndnQVBXUnV3Nmd0dlNVUmhsTUQxRXMxcEhid3kzV3VVQ0dnSlc5R2FrbDFVK0VRWXczR1R1K3dqRTFCT1kyL0N1KzQyUkQ5bjFvbUNseDgrUmtjdWl1WU1FdlhXMFhRdXV2V1NycFo1NmMrekd3aWxtQll1WGJkTkk4b2NrUEZCdHR5TCtHbmNybFZRS3RVYmVwNUhLbDlDMVFtTkxjYzl6OUlOUDBtM1UvaGJiNnBRS0xYQmhUemZDQnhIdmc3S3ZaeXV2eVFSRXJnTUdKb3NLWkFGNHpNM1p5TENOa0lNNWxhSDRFVTFWNzdRUmdqbDVHdlV5Z0dJY3diUVd0V00vUTF2dXFCd09xd2R2K3FwNk5Ta0l5dmVoQURmWmxkd0N2eC9HdlZtcTB6a21tMVlhN1F1S2tFbXp3cmRzZTAxSmdYbG1TSGpaQXI4NXRTWEFRWHVWeDV0MXNjVUtrTm1qVXoveWhtV1AxU0dkeVZEenZhYUcxTkI0eTNKNUZMOFlBTjM0WU9hbHFWTy9xU095VmYybktqWEtWaEJWK29SUVNKNGRvcmVka1dsQ3pMbjdBTUlCSzZKSXVTQ1lUMWx4MTd5TDF3Yk1DajUvZmxQYkhzZ0RkVlJHN2lVc1ZFZnVNaTJ6dndUV3VnTjgraG91ZTRxa2FXZG42QTFTTVZ4R1ZPSDBjSWlSM2VPZVZBWUNUN0VMUDc0MkVXMEY4SHlMRExBeTkwOVUyS1NGd00vbHh3eEJZZCtINzFXV1lFTWVzdjdhWlZTZjVmelJkSnVEK1ZhTXhBMU9IVFN0cFNvWHp5SzFRcXc1NUY2KzZBMFJnZk1MY3ZrdDRQVzMzU0FvVVQ3VWtiLzN4QVM3MlJkUTN4dHdSQ3FHN0hxc3A1UlduVWQ1L0R6amJlMmRraVNPbGF3UW03cCs3b0hSZWRoVjZINVdweWlwajJScHlDSnVpRE82VW8zcnAvZVREVHhwS3Q4cVBKZllJZ21LNktvMVl6T2RlVEVXOUdpaUh4Z3JtOWM3S1A4WmJidm0rQVk4cURJOXRLWHdLcjgzNVRlOHBNT2MxNjNmZHdYd2tDdzZnWVFPeHloYmhRV3VrUFYyK3prbXJoaEVOeGlseitibDFRWHl4V2hSTUFzYm9udU9jcGJ2VWVvbytSSXh1TGpQbEhncHY1Q0xSWmR0azBuVUxMU1ZES1k4PS0tNVJWM0dmSVFEOEdmOVE0bnMxSWFEdz09--be84140490b1b6ef38066a42f7fc49a121559850";

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
