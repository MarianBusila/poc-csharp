using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publisher;

string projectId = "unity-solutions-pwest-test";
string topicId = "test-marian";

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddPublisherClient(TopicName.FromProjectTopic(projectId, topicId));
        services.AddPublisherServiceApiClient();
        
        services.AddTransient<IPublisher, PubSubPublisher>();
    })
    .Build();

var publisher = host.Services.GetRequiredService<IPublisher>();
await publisher.CreateTopic(projectId, topicId);
await publisher.PublishMessage();



