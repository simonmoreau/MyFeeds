using System;
using System.Configuration;
using System.ServiceModel.Syndication;
using System.Xml;
using Azure.Storage.Blobs;
using Google.Protobuf.Compiler;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MyFeeds
{
    public class FeedConverter
    {
        private readonly ILogger _logger;

        public FeedConverter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FeedConverter>();
        }

        [Function(nameof(FeedConverter))]
        public async Task Run(
            [TimerTrigger("0 0 */4 * * *" 
#if DEBUG
            , RunOnStartup=true
#endif
            )] TimerInfo myTimer,
            [BlobInput("feeds")] BlobContainerClient blobContainerClient)
        {
            try
            {
                _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

                if (myTimer.ScheduleStatus is not null)
                {
                    _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
                }

                List<Feed> feeds = await GetAllFeeds();

                foreach (Feed feed in feeds)
                {
                    string fileName = $"{feed.FeedId}.xml";
                    BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

                    using (Stream stream = feed.WriteFeed())
                    {
                        blobClient.Upload(stream,overwrite:true);
                    }
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);

                throw;
            }

        }

        private async Task<List<Feed>> GetAllFeeds()
        {

            Type type = typeof(Feed);
            List<Type> allFeedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && p.Name != nameof(Feed)).ToList();


            List<Task<Feed>> tasks = new List<Task<Feed>>();
            
            foreach (Type feedType in allFeedTypes)
            {
                tasks.Add(LoadFeed(feedType));
            }

            List<Feed> feeds = (await Task.WhenAll<Feed>(tasks.ToArray())).ToList();
            return feeds;
        }

        private async Task<Feed> LoadFeed(Type feedType)
        {
            Feed feed = (Feed)Activator.CreateInstance(feedType);
            await feed.BuildFeed();

            return feed;
        }

    }
}
