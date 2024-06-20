using Microsoft.Extensions.Logging.Abstractions;
using MyFeeds.Feeds;


namespace MyFeedsTests
{
    public class PermanentStyleTest
    {
        [Fact]
        public async Task Load()
        {
            PermanentStyle permanentStyle = new MyFeeds.Feeds.PermanentStyle(new NullLoggerFactory());
            List<MyFeeds.Feed> feeds = await permanentStyle.GetFeeds();

            Assert.True(feeds.First().Articles.Count > 0);

        }
    }
}