using MyFeeds.Feeds;


namespace MyFeedsTests
{
    public class BonneGueuleTest
    {
        [Fact]
        public async Task Load()
        {
            BonneGueule bonneGueule = new MyFeeds.Feeds.BonneGueule();
            await bonneGueule.BuildFeed();

            Assert.True(bonneGueule.Articles.Count > 0);

        }
    }
}