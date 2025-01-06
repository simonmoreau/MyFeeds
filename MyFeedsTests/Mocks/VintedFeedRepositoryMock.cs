using MyFeeds.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MyFeedsTests.Mocks
{
    internal class VintedFeedRepositoryMock : IVintedFeedRepository
    {
        public Task<List<VintedFeed>> GetFeedInputs()
        {
            return Task.FromResult(new List<VintedFeed>() {
                new VintedFeed()
                {
                    Name = "suitsupply",
                    Url = $"catalog_ids=2050&brand_ids=313302",
                }
            });
        }
    }
}
