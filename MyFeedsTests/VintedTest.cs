using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyFeeds.Clients;
using MyFeeds.Feeds;


namespace MyFeedsTests
{
    public class VintedTest
    {
        [Fact]
        public async Task Load()
        {

            ILogger<VintedDelegatingHandler> logger = new NullLoggerFactory().CreateLogger<VintedDelegatingHandler>();

            HttpClientHandler httpClientHandler = new HttpClientHandler() { UseCookies = false };
            VintedAuthenticationClient vintedAuthenticationClient = new VintedAuthenticationClient(new HttpClient());

            VintedDelegatingHandler vintedDelegatingHandler = new VintedDelegatingHandler(httpClientHandler, vintedAuthenticationClient, logger );
            VintedClient vintedClient = new VintedClient(new HttpClient(vintedDelegatingHandler));
            Vinted vinted = new MyFeeds.Feeds.Vinted(vintedClient, new NullLoggerFactory());
            List<MyFeeds.Feed> feeds = await vinted.GetFeeds();

            Assert.True(feeds.First().Articles.Count > 0);

        }

        [Fact]
        public async Task Authentication()
        {

            ILogger<VintedDelegatingHandler> logger = new NullLoggerFactory().CreateLogger<VintedDelegatingHandler>();

            HttpClientHandler httpClientHandler = new HttpClientHandler() { UseCookies = false };

            VintedAuthenticationClient vintedAuthenticationClient = new VintedAuthenticationClient(new HttpClient());

            string sessionCookie = await vintedAuthenticationClient.GetSessionCookie();

            Assert.NotNull(sessionCookie);

        }
    }
}