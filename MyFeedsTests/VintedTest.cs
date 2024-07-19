using Azure.Data.Tables;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyFeeds.Clients;
using MyFeeds.Feeds;
using MyFeeds.Utilities;
using MyFeedsTests.Mocks;


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

            ICycleManager cycleManager = new CycleManagerMock();
            IVintedFeedRepository vintedFeedRepository = new VintedFeedRepositoryMock();

            Vinted vinted = new MyFeeds.Feeds.Vinted(vintedClient, new NullLoggerFactory(), cycleManager, vintedFeedRepository);
            List<MyFeeds.Feed> feeds = await vinted.GetFeeds();

            Assert.Single(feeds);
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

        [Fact]
        public async Task GetFeedInput()
        {
            Environment.SetEnvironmentVariable("VintedSizes", "size_ids=208,208,213,1637,1638,1652,1547,1611,1594,784,1524");

            TableServiceClient tableServiceClient = new TableServiceClient("UseDevelopmentStorage=true");
            IVintedFeedRepository vintedFeedRepository = new VintedFeedRepository(tableServiceClient);
            List<VintedFeed> inputs = await vintedFeedRepository.GetFeedInputs();

            Assert.NotEmpty(inputs);
        }
    }
}