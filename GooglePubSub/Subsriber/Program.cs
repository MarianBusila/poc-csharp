using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subsriber;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        SubscriptionConfiguration? subscriptionConfiguration =
            configuration.GetSection("SubscriptionConfiguration").Get<SubscriptionConfiguration>();
        
        SubscriptionName subscriptionName = new SubscriptionName(subscriptionConfiguration.ProjectId, subscriptionConfiguration.SubscriptionId);
        services.AddSubscriberClient(subscriptionName);
        services.AddSubscriberServiceApiClient();
        services.AddHostedService<SubscriberService>();
        
        services.Configure<SubscriptionConfiguration>(
            configuration.GetSection("SubscriptionConfiguration"));

        
    })
    .Build();

await host.RunAsync();
