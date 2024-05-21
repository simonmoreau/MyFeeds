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
        [BlobOutput("test-samples-output/output.xml")]
        public string Run(
            [TimerTrigger("0 */5 * * * *" 
#if DEBUG
            , RunOnStartup=true
#endif
            )] TimerInfo myTimer)
        {
            try
            {
                _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

                if (myTimer.ScheduleStatus is not null)
                {
                    _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
                }

                List<Feed> feeds = GetAllFeeds();

                // Get a connection string to your Azure Storage account
                string connectionString = Environment.GetEnvironmentVariable("CUSTOMCONNSTR_DefaultConnection");

                // Get a reference to the blob container where you want to upload the file
                string containerName = "test-samples-output";
                BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                // Upload the XML file to Azure Blob Storage

                foreach (Feed feed in feeds)
                {
                    string fileName = $"{feed.FeedId}.xml";
                    string filePath = $"{feed.FeedId}.xml";
                    BlobClient blobClient = containerClient.GetBlobClient(fileName);

                    using (Stream stream = feed.WriteFeed())
                    {
                        blobClient.Upload(stream);
                    }
                    
                }


                return "feeds";
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
