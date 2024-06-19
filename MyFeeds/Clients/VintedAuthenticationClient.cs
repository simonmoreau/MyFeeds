using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyFeeds.Clients
{
    public class VintedAuthenticationClient
    {
        public HttpClient Client { get; }

        private string? _cachedCookie;

        public VintedAuthenticationClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.vinted.fr/");

            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");
            
            Client = client;
        }

        public async Task<string> GetSessionCookie()
        {
            if (!string.IsNullOrEmpty(_cachedCookie))
            {
                return _cachedCookie;
            }

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "/");

            HttpResponseMessage response = await Client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            if (cookies == null)
            {
                return "";
            }

            foreach (string cookie in cookies)
            {
                if (cookie.Contains("_vinted_fr_session"))
                {
                    string pattern = @"_vinted_fr_session=(.*?);";
                    Match match = Regex.Match(cookie, pattern);
                    if (!match.Success) { continue; }
                    _cachedCookie = match.Groups[1].Value;
                    return _cachedCookie;
                }
            }
            return "";
        }

        public void ClearCachedCookie()
        {
            _cachedCookie = null;
        }

    }
}
