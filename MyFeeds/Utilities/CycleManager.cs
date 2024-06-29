using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyFeeds.Utilities
{
    public class CycleManager : ICycleManager
    {
        private readonly TableServiceClient _tableServiceClient;

        private long _cycleNumber;
        public CycleManager(TableServiceClient tableServiceClient)
        {
            _tableServiceClient = tableServiceClient;
        }

        public async Task ReccordRun()
        {
            await _tableServiceClient.CreateTableIfNotExistsAsync(nameof(Cycle));

            TableClient tableClient = _tableServiceClient.GetTableClient(nameof(Cycle));

            AsyncPageable<Cycle> queryResults = tableClient.QueryAsync<Cycle>(c => c.PartitionKey == nameof(Cycle) && c.RowKey == nameof(Cycle));

            List<Cycle> cycles = await queryResults.ToListAsync<Cycle>();

            if (cycles.Count > 0)
            {
                Cycle updatedCycle = cycles.First();
                updatedCycle.CycleNumber = updatedCycle.CycleNumber + 1;
                _cycleNumber = updatedCycle.CycleNumber;
                await tableClient.UpdateEntityAsync(updatedCycle, updatedCycle.ETag);
            }
            else
            {
                Cycle newCycle = new Cycle()
                {
                    PartitionKey = nameof(Cycle),
                    RowKey = nameof(Cycle),
                    CycleNumber = 1
                };
                _cycleNumber = newCycle.CycleNumber;

                await tableClient.AddEntityAsync(newCycle);
            }

            
        }


        public bool CanRun(int index, int count)
        {
            var modulo = _cycleNumber % count;

            if (modulo == index) return true;
            return false;
        }
    }
}
