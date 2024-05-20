using System;
using System.ServiceModel.Syndication;
using System.Xml;
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

        [Function("Function1")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }

        private void LoadReader()
        {
            var type = typeof(IMyInterface);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
        }


    }
}
