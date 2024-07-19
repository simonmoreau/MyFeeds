using Azure.Data.Tables;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MyFeeds.Clients
{
    public class VintedFeedRepository : IVintedFeedRepository
    {
        private readonly TableServiceClient _tableServiceClient;

        private long _cycleNumber;
        public VintedFeedRepository(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        public async Task<List<VintedFeed>> GetFeedInputs()
        {
            TableClient tableClient = _tableServiceClient.GetTableClient(nameof(VintedFeedRepository));

            AsyncPageable<VintedFeed> queryResults = tableClient.QueryAsync<VintedFeed>(c => c.PartitionKey == nameof(VintedFeed));

            List<VintedFeed> feeds = await queryResults.ToListAsync<VintedFeed>();

            string? sizes = Environment.GetEnvironmentVariable("VintedSizes", EnvironmentVariableTarget.Process);

            if (sizes != null)
            {
                foreach (VintedFeed vintedFeed in feeds)
                {
                    System.Collections.Specialized.NameValueCollection paramsCollection = HttpUtility.ParseQueryString(vintedFeed.Url);
                    if (paramsCollection != null && paramsCollection.AllKeys.Contains("size_ids"))
                    {
                        continue;
                    }
                    else
                    {
                        vintedFeed.Url = vintedFeed.Url + "&" + sizes;
                    }
                }
            }

            return feeds;
        }
    }

}
