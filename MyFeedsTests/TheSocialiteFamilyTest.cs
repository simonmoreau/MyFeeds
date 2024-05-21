using MyFeeds.Feeds;


namespace MyFeedsTests
{
    public class TheSocialiteFamilyTest
    {
        [Fact]
        public async Task Load()
        {
            TheSocialiteFamily theSocialiteFamily = new MyFeeds.Feeds.TheSocialiteFamily();
            await theSocialiteFamily.BuildFeed();

            Assert.True(theSocialiteFamily.Articles.Count > 0);

        }
    }
}