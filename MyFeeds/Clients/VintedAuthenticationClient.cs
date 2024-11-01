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

        private VintedCookie? _cachedCookie;

        public VintedAuthenticationClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.vinted.fr/");

            // GitHub requires a user-agent
            client.DefaultRequestHeaders.Add("User-Agent",
                "HttpClientFactory-Sample");
            
            Client = client;
        }

        public async Task<VintedCookie?> GetSessionCookie()
        {
            if (_cachedCookie != null)
            {
                return _cachedCookie;
            }

            _cachedCookie = new VintedCookie();

            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "/");

            HttpResponseMessage response = await Client.SendAsync(message);
            response.EnsureSuccessStatusCode();

            IEnumerable<string> cookies = response.Headers.SingleOrDefault(header => header.Key == "Set-Cookie").Value;
            if (cookies == null)
            {
                return null;
            }

            foreach (string cookie in cookies)
            {
                string? session = GetCookieValue(cookie, "_vinted_fr_session");
                if (session != null) _cachedCookie.Session = session;

                string? acessToken = GetCookieValue(cookie, "access_token_web");
                if (acessToken != null) _cachedCookie.AccessToken = acessToken;

                string? refreshToken = GetCookieValue(cookie, "refresh_token_web");
                if (refreshToken != null) _cachedCookie.RefreshToken = refreshToken;

            }

            if (!_cachedCookie.IsValid()) return null;

            return _cachedCookie;
        }

        private string? GetCookieValue(string cookie, string value)
        {
            if (!cookie.Contains(value))
            {
                return null;
            }

            string pattern = @$"{value}=(.*?);";
            Match match = Regex.Match(cookie, pattern);
            if (!match.Success) { return null; }
            return match.Groups[1].Value;
        }


        public void ClearCachedCookie()
        {
            _cachedCookie = null;
        }

    }

    public class VintedCookie
    {
        public string? Session { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }

        public bool IsValid()
        {
            return Session != null && AccessToken != null && RefreshToken != null;
        }
    }
}
