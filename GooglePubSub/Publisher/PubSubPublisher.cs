using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Publisher;

public class PubSubPublisher : IPublisher
{
    private readonly PublisherClient _publisherClient;
    private readonly PublisherServiceApiClient _publisherServiceApiClient;
    private readonly ILogger<PubSubPublisher> _logger;
    private readonly TopicConfiguration _topicConfiguration;

    public PubSubPublisher(PublisherClient publisherClient, PublisherServiceApiClient publisherServiceApiClient, ILogger<PubSubPublisher> logger, IOptions<TopicConfiguration> topicConfigurationOptions)
    {
        _publisherClient = publisherClient;
        _publisherServiceApiClient = publisherServiceApiClient;
        _logger = logger;
        _topicConfiguration = topicConfigurationOptions.Value;
    }

    public async Task CreateTopic()
    {
        try {
            _logger.LogInformation("Creating topic ...");
            var topicName = new TopicName(_topicConfiguration.ProjectId, _topicConfiguration.TopicId);
            await _publisherServiceApiClient.CreateTopicAsync(topicName);
            _logger.LogInformation("Topic created");
        }
        catch (Grpc.Core.RpcException)
        {
            _logger.LogWarning("Topic already exists");
        }
    }
    
    public async Task PublishMessage()
    {
        var messageId = await _publisherClient.PublishAsync("Hello at " + DateTime.Now);
        _logger.LogInformation("Published MessageId: " + messageId);
        await _publisherClient.ShutdownAsync(TimeSpan.FromSeconds(5));
    }
}