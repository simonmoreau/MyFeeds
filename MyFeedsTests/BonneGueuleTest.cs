using MyFeeds.Feeds;


namespace MyFeedsTests
{
    public class BonneGueuleTest
    {
        [Fact]
        public async Task Load()
        {
            BonneGueule bonneGueule = new MyFeeds.Feeds.BonneGueule();
            List<MyFeeds.Feed> feeds = await bonneGueule.GetFeeds();

            Assert.True(feeds.First().Articles.Count > 0);

        }
    }
}