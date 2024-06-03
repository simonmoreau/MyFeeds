using MyFeeds.Clients;
using MyFeeds.Feeds;


namespace MyFeedsTests
{
    public class VintedTest
    {
        [Fact]
        public async Task Load()
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler() { UseCookies = false };
            VintedClient vintedClient = new VintedClient(new HttpClient(httpClientHandler));
            Vinted vinted = new MyFeeds.Feeds.Vinted(vintedClient);
            bool result = await vinted.BuildFeed();

            Assert.True(vinted.Articles.Count > 0);

        }
    }
}