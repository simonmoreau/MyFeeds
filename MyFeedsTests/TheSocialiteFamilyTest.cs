using Microsoft.Extensions.Logging.Abstractions;
using MyFeeds;
using MyFeeds.Feeds;


namespace MyFeedsTests
{
    public class TheSocialiteFamilyTest
    {
        [Fact]
        public async Task Load()
        {
            TheSocialiteFamily theSocialiteFamily = new MyFeeds.Feeds.TheSocialiteFamily(new NullLoggerFactory());
            List<MyFeeds.Feed> feeds = await theSocialiteFamily.GetFeeds();

            Assert.True(feeds.First().Articles.Count > 0);

        }
    }
}