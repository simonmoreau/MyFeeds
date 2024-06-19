using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFeeds.Clients;
using System.Net;

IHost host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddHttpClient<VintedClient>()
        .ConfigurePrimaryHttpMessageHandler(
            () => new HttpClientHandler() { UseCookies = false }
            )
        .AddHttpMessageHandler<VintedDelegatingHandler>();
    })
    .Build();

host.Run();
