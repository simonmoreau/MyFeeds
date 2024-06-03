﻿using System;
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
        private readonly string cookie = "M1ZDWFNMajNjV0JXMExWM1ZRbTJSUTlnenhkQzhvazBxeHJicm9RVGZpN2hyd2orZUl1MitiVTJNTVV4S3ppNURBbHo4NHJ6ekUwNkNvSWtFaUk1QzBhVURrZDY0SWdaWWpPQWVMYnlBeXB1aE5NOTZna1NLSTJxOGJ4UWtnS2JDbmZhR3ZnZjZ2OC9wQmhyckl6RldmNHNIVThQeVhSNjA4d1A4b1VpNUtXYjlmMy94aUVIRjFqa2E4ZEVBdDhKQ1E4aHFGZi9PdnVOTGtyZmFPeHBLcE1PZmY5ZWMwRHVWRkdoUituMytjYXl6THlFOTZHdFFxSUNzSEdXZjlhK1FFUWZiRmx2aWJyL1BmRWgyTXVqOWpLdlJaNksxbXJpWWpobGNPWVRDUXhkajhlT0h4Z3h3Y2I2U2ZoUjA0NVJGRE9FTkpLSS92M3NDVjd6UEZUbURVbUJGTUp1N0xmSzVQekJkYW5qYStjSUx1WWtzTTVwMEdiSDNvZkQ1ektoS09iWjh1OVg3dE1GS2lpRERaMk9XTU1vTHFhZ0hhMkNGWldLdnUrTGMxU0wzcjl6S0VVQnBBb2RVUWxKZGhkVXRMaTJaQld5TXZwRXA5YlNla3R0ZmZEb2VBdkFsZ0h6aFNpMVNFOVFTZ09LeHo0eUpzZGl5RWVIMlhENzh2angxZDBiYlB4dTVNdDl3N1lxYVVJNUQ2ZTU4TmlldjRObUcrbFVDQ3Z6Wmo1cGxUdlYwWDhjMDlEcFNvYUUwc2NFanpzRTZIWjNYYVFpMEhHSzVRcTV1ME9FVExFQnFTWEVNckF1c1Z1Q1JyQW9kV2Jjdno0ZU84dVZqWUdBVXUzS2ZDWkVMK2VrNk1FcGRPM0Q2MFA4MWNLY1UwdGpsRGFJYVp6MEpSalNuNzF2MGkvMFF4Y3E0aHY1N25sdGZNMjdUaWp1SVc5NGo1N3JaZ29ZNVd1c3dBZlZxZEdFN3JJWWV4eEM1dFNtZmJZcnhicXlsRGh1R1pINEkxam5OcmpNdkZMa3dtdDVPTHVjL3p5cmoySE5mN3JWRE5rWmdvQU5xUHRDL29kbzlTZVhJcFI4RWd5R2dJMzEvV0RDelUvd3RkVUtMZHhlVmlkemFsV3lUdXlkcXhOOHJzZ0piTzJqY1VFTTBSd3ovRnhtVW0vMWREQm00a0FsS2M2SzZwdmFRWHlZVEtrdGsxaFNPZGovR3dFNUJ0OTIrN0ViS0syNHh1ZktpQktuL2JSeHFlQVozUXNDWXFDRFRMKzFEemx2cDlESlZFMG9idUVsNzFGYk93SWR3SVpRN0xTQjVmUmJWaDRxRko5UnhhQmFtUUdNMkxLNml5TW1ka2lZOTNvSnlBNkZSVVRzNXJBaTlGLzhwVFBSdzFVREpaVWFJQ3VWSXR4aUFIVUhPZHlzaFVlYU9hR1NtUU8rS1p2VGNzSzVSNGFGRHpaZzgrcHJ5RGc0V1pKNkY1N3lJUFdzMUxGMFAzY2RkMXdGcUVSNTFTM1RuREhGOEdKOXl1dEZuSGhwbzlRRDh2N3dhZUtSckxsSGtpN21PSWQ4WmpDOSsrMmE5Mit6WjFvck1KbkNVdXNiblllUDMvd3NVQmhpcytpVzRyRnBXbldiMTZUTlNJeVhiQ0R6LzFUeTU2YWtuUEI1Sy9PZ0orMVlUMVVNSEdRczR3VjdqSitRUVN3ZldYS3JrbG9PaEN2RGs1U1JuRFFOci82czNrcjk3aE9GM05WWDU4RUVXT2luRWVsMzRXNThRS2JTWFpEUlUvNHJvNDZwT3VOOXhTMzVUeFlFUFp0MTIrNVhKc3lVOWJBRWQxaDk4ak9hRjdqejZRUkRwcVJHa0x5TVFkY29KKzNkNUtGaGVLZnVpWmRMVlVYcm1EaG55YURkK2wzVFZaYlZURUVCTHpGM1pqVVlVc3k2dzQ4Y3JVVkM1bTRXNkMvYmdoMWpSbW1RZXR2TXI4cHZMRDE0am5RZVF0M3hudHVjeGxqelI5U1E5QjloWGNmc2tTNkhQRVlOMkVEUE1vczZXUmMxL0ZCdUloTVFZSDdnaWFiaW5URnlyaDRJOFlmU2MyemJDK1AzcmJ0V3k5RmQ0emNCaG5zUVh2dmp4VWExMzlnRVoyclBFMkJUNVZ2QnI2RzBvOHVpMitSOVB5ODdyT2RWZ1UwRGlDN3N4WDV4b2RzYWtIT0lOYlZEdFB4cCthbjIwUzVuV21SN2NHWjM3Vk5Na2RPbFhBb2pKdE91YVR0c3lkUkFuY3VvdkJoWUxURGJNVUJLdWx1cUtoOEpGei80blZ1NUlIYWU0ZG5obkdpR0lTdGFxMktZY0VLMVdSQ3U2ZThtblE3K0xMZVA4cWNueitPSkFNQ00vSVpNTmsvQVNzd2tlR0pYMHZqelNlajFLUXJueXUyUXhHOGpmeTk5NVNSSkxPVklVdXBlRy9qUVptV1BKR2duRmN3ZDNqaVNIdWJlVkpoOHJoZUl5RVhEZllSNTIxdlp2QVMwRGY1MklGVmhNbzZYSkRBPS0tem5EWnVKeW92TlRrcHh6Y25aemp3dz09--547dbd3f8ad42b695bfce7aaa48635b08ab1c557";

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
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "api/v2/catalog/items?page=1&per_page=96&search_text=Inis+Me%C3%A1in&catalog_ids=2050&size_ids=207,208,209&brand_ids=&status_ids=&color_ids=&material_ids=");
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
