using System;
using System.Configuration;
using System.ServiceModel.Syndication;
using System.Threading.Tasks;
using System.Xml;
using Azure;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Google.Protobuf.Compiler;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyFeeds.Utilities;

namespace MyFeeds
{
    public class FeedConverter
    {
        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        public FeedConverter(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<FeedConverter>();
            _serviceProvider = serviceProvider;
        }

        [Function(nameof(FeedConverter))]
        [TableOutput("Cycle", "Cycle", "cycle-number")]
        public async Task<Cycle> Run(
            [TimerTrigger("0 0 */4 * * *" 
#if DEBUG
            , RunOnStartup=true
#endif
            )] TimerInfo myTimer,
            [BlobInput("feeds")] BlobContainerClient blobContainerClient,
            [TableInput("Cycle")] TableClient tableClient)
        {
            try
            {
                _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

                if (myTimer.ScheduleStatus is not null)
                {
                    _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
                }

                //await GetAllFeedBuilders(blobContainerClient);
                await tableClient.CreateIfNotExistsAsync();

                // filter: $"PartitionKey eq 'PartitionKey' and RowKey eq 'cycleInput'"

                AsyncPageable<Cycle> queryResults = tableClient.QueryAsync<Cycle>(c => c.PartitionKey == nameof(Cycle) && c.RowKey == nameof(Cycle));

                List<Cycle> cycles = await queryResults.ToListAsync<Cycle>();

                if (cycles.Count > 0)
                {
                    cycles.First().CycleNumber = cycles.First().CycleNumber + 1;
                    return cycles.First();
                }
                else
                {
                    return new Cycle()
                    {
                        PartitionKey = nameof(Cycle),
                        RowKey = nameof(Cycle),
                        CycleNumber = 1
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);

                throw;
            }

        }

        private async Task GetAllFeedBuilders(BlobContainerClient blobContainerClient)
        {

            Type type = typeof(FeedBuilder);
            List<Type> allFeedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p) && p.Name != nameof(FeedBuilder)).ToList();

            List<Task> tasks = new List<Task>();
            
            foreach (Type feedType in allFeedTypes)
            {
                FeedBuilder builder = (FeedBuilder)ActivatorUtilities.CreateInstance(_serviceProvider, feedType);
                Task writeFeedTask = builder.WriteFeeds(blobContainerClient);

                tasks.Add(writeFeedTask);
            }

            await Task.WhenAll(tasks);
        }


    }
}
