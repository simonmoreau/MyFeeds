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
        private readonly string cookie = "a2cwVk1OU1pKYktEOFYvVWdKQzhiVjl6V3g0UVJ3T2pLakkvTzFwKzJSV3g1aUEzZTcwYzhER01mY2V2TWRvaklIM1orQmxKakhHMzZqQWk5d1RhZHdHNTY4SEpIa25veEVSYzB4TCtubkl1a0loNm9LZTVRQTcyUHg0SlpYYUkxQWFDcWM4ZTIrODEvSEVOMElsSmJrSE1ubEJwc29ZQWc1d3VvWERJVXluNDUxSW00NUx2a0NYbklUNGRDODNoZGp0bndoWFZtRnZWTlliNjl3UHJkQkRsYnR0TkxkeW9qcHhuOEp1WG1BRTg3VFVvQ0dpVkJFbi9YRERaa3JiSTc5NjFtb2w0Zkd0c0hWRUpGc292R29VTmkyZ3p5dEJaNDNlZWRuT1hpRXVNb0tyN3N3MHM4Z1lBL3FFZ1o2NUdJMkJlOFh5U0dNYWRNbExnQWdmbFFIQ0k0aC9PaHFqUWlmTTMwVHN5SmJBYkFkdHgvN3V1a2RsTWpJQjhJTXVjZGxLc2NKRmtNdGVMSVFKUG52ZFVDNFIralF1K2l0S00yaWFxWExRNzVxYXlzVWR4ZGpveWF2VW41c2xzb2FtbC9acVRGanRnUTlPSE9qa1BlMWMzMmc2N0FPdWNEK2oyM3dOcGZZbGg3aXpld2JzU0pJMEEwOXFUcW80eWxZTzh0TS9hdzQ1cFpVWk5Lc1NGZXVXaVFxamU1K0R1WGRNMlhwQ25SUUJwMTBmMTNQUEhCTlFhUjZ0M0tvbmI1eGdwT2hUUG55b3FsTGticGFYdE5HWFdnTzVTbnBHUVpaUDBwcGxnMWhRU3AxRTc4VWpRMCt4c21LQW85a1JqazROc1dZWEIwZGhFY29NaEFOZTBnTElrRVlaYUNTM0liV3E4YkZtR21OQWFnTTVlQWIwTDNIYkNMeWdtMlpJV09xMEVuVy9LV0ZKdm5TMXUwWmt2czNkSzZRa0pINVR2YU5lbGNpVG9oTFBvKzZkSHdTSHNLakNPV08zaWk2bVkzanZSMGJEdUs2UXBsTUpWc0VMWUxHdHBtRGZDYlphcmpFUG13ckd1MVFXRUdsYXZpbHQyQUpkM3RwU1JrczVnMVlVeTZwRHF0bS80anV5bnk2QitMSjAzNVJXRFNzZXJXbkU1Y1ljYXo3VkhmUFdKdERUSEZWdExTNDN1TkoxSjNqTXdabXZYUm4rWEhxQVRyMFQ0SXFSRG9oSzBiaGVoaUpsOXY3SWJqZEJNTlpwMWNMd0lESWZSc1FpZGVIMmFhdFJWK0p3dDNRbi96dDVtTGJYdlNGRmhmUE1VejEvTzFLWElka3NUTWdjNWxlY1ZSL2tydXN3WjBoQ2Z5VENCckRyNW9xcXdMbXNRb2FsNTFHb0wvTDVQa3ZqRUs4ZHBhSzV3OW56NzdaV2lvUVdIdGZXb3Rrcy9JUmEyczVMRFkxMGZmeTlvMjNJNU5wV2Vjd1UwQjIxeUl4MDhaSGlDVWFKd3dQZlNPVzFNbVh6WmZkc0VydW04R0xjdWptTnhydlhlaTlEUjF1bGdnTjZWUjRtVjNVb0RQaE05dEJXTy95OFowWEtXd3AyN1FDTGZjOExzcGxocXQ2ak40SGRKdnU2TEZRLyttZmpCbmFhL1dlRkFqekZOdkEwdDFBV2pGMjFrWDl1T1RNRDdsbVg1OStoSFFGSlc3Y3pmZ2QrRFpUV21VT25pRGxhMUtRZlVCNGdmLzVaSEZ0clYzaVhPRG13ZVJ5R2pyLy9YSHZVaTBXU3phYjllNk9HZnRZSVRlUHJBc0x4YXdST0twdEpta1JjRkJkNGRQaHZCcFRiRmc3ZWg5c1JhaWdxaDNzSzZPbUNzOHlyWENlYkoxVGtlejhGckZuTTk5Smc5RXM0OHU5Zm9LRWhKUlFGbm8xNVlaSFY0L21EcjlHYUkwYjVocWQ1citVZmV0b0NkT21XMUZtQk9RUXozQ3JSZ3h5aUxaeVFOYk5PM2JVZEo4eHB0NGxnVHpKc29aeE4vUU1aTWpwejgxbjRtV1NaYWZtUGFEaFNPT3F0ZkVHbzFJQXVYTkh5STczWkx6VVJMS1lkZjZDQWxHcEk2RjF1UXFyRjd5VENweDFIY3BKbVJxRTFjSEk4ZVpTMnpTdkdZSmZPZWRJZE44SmhjSXFZNEQ2a3ppc0xkd2hHVEVmSGpxL0prK3NpNFlFYUtPVlVadVBMaXlwWmUxWnV6eGR2TWNoK2MrOVI4cVhURVZBQkZROG5LWnhwbmt2aGxITmhHMWhGK1dsWGNqQWxjMm1pd2RNZ0JDZElydThhZ0IzYkg5UXBaazFZSTFkNDZ3Z3JsWXFFSmhhajJmSnR3NHRVd1dtby9WMXJ2ZnRZZjF5bUd0QXlxLzBRWHdOL21hVk80MjlyeGFUdko0bko3MjlKSjZzNTB3M1QzdXdQWDRaM2lna1dJM3hqZEZ4NExZb3lMZGZTTnFkRVBjcU9ZUjZwa3FwK0labzEvQ2RVZUx3RisweE5uRi9HS1NPd0RDblJNTHVJPS0teXdjc0tOdGpxRjdTeGJrWXdMZDRnUT09--640c61350e74c9897ba80b8edf11d11a4ecfa4b4";

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
