using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyFeeds.Clients;
using MyFeeds.Utilities;
using System.Net;


IHost host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddAzureClients(clientBuilder => {
            clientBuilder.AddTableServiceClient(Environment.GetEnvironmentVariable("AzureWebJobsStorage", EnvironmentVariableTarget.Process));
        });
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddScoped<VintedDelegatingHandler>();

        services.AddScoped<ICycleManager, CycleManager>();
        services.AddScoped<IVintedFeedRepository, VintedFeedRepository>();
        

        services.AddHttpClient<VintedAuthenticationClient>();
        services.AddHttpClient<VintedClient>()
        .ConfigurePrimaryHttpMessageHandler(
            () => new HttpClientHandler() { UseCookies = false }
            )
        .AddHttpMessageHandler<VintedDelegatingHandler>();
    })
    .Build();

host.Run();
