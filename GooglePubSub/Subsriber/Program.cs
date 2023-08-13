using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subsriber;

string projectId = "unity-solutions-pwest-test";
string topicId = "test-marian";
string subscriptionId = "test-marian-subscription";

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        SubscriptionName subscriptionName = new SubscriptionName(projectId, subscriptionId);
        services.AddSubscriberClient(subscriptionName);
        services.AddSubscriberServiceApiClient();
        services.AddHostedService<SubscriberService>();
    })
    .Build();

await host.RunAsync();
