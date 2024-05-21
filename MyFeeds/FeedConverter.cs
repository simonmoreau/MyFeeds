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
        public void Run(
            [TimerTrigger("0 */5 * * * *" 
#if DEBUG
            , RunOnStartup=true
#endif
            )] TimerInfo myTimer,
            [BlobInput("test-samples-output")] BlobContainerClient blobContainerClient)
        {
            try
            {
                _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

                if (myTimer.ScheduleStatus is not null)
                {
                    _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
                }

                List<Feed> feeds = GetAllFeeds();

                foreach (Feed feed in feeds)
                {
                    string fileName = $"{feed.FeedId}.xml";
                    string filePath = $"{feed.FeedId}.xml";
                    BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

                    using (Stream stream = feed.WriteFeed())
                    {
                        blobClient.Upload(stream);
                    }
                }
            }

            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);

                throw;
            }

        }

        private List<Feed> GetAllFeeds()
        {
            List<Feed> feeds = new List<Feed>();

            Type type = typeof(Feed);
            List<Type> allFeedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && p.Name != nameof(Feed)).ToList();



            foreach (Type feedType in allFeedTypes)
            {
                Feed feed = (Feed)Activator.CreateInstance(feedType);
                feeds.Add(feed);
            }

            return feeds;
        }


    }
}
