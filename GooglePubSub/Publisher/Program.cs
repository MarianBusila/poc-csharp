using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Publisher;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(builder =>
    {
        builder.AddJsonFile("appsettings.json");
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        TopicConfiguration? topicConfiguration =
            configuration.GetSection("TopicConfiguration").Get<TopicConfiguration>();
        
        services.AddPublisherClient(TopicName.FromProjectTopic(topicConfiguration.ProjectId, topicConfiguration.TopicId));
        services.AddPublisherServiceApiClient();
        
        services.Configure<TopicConfiguration>(
            configuration.GetSection("TopicConfiguration"));
        
        services.AddTransient<IPublisher, PubSubPublisher>();
    })
    .Build();

var publisher = host.Services.GetRequiredService<IPublisher>();
await publisher.CreateTopic();
await publisher.PublishMessage();



