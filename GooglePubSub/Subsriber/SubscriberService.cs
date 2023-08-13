using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Subsriber;

public class SubscriberService : BackgroundService
{
    private readonly SubscriberClient _subscriberClient;
    private readonly SubscriberServiceApiClient _subscriberServiceApiClient;
    private readonly ILogger<SubscriberService> _logger;
    private readonly SubscriptionConfiguration _subscriptionConfiguration;

    public SubscriberService(SubscriberClient subscriberClient, ILogger<SubscriberService> logger, SubscriberServiceApiClient subscriberServiceApiClient, IOptions<SubscriptionConfiguration> subscriptionConfigurationOptions)
    {
        _subscriberClient = subscriberClient;
        _logger = logger;
        _subscriberServiceApiClient = subscriberServiceApiClient;
        _subscriptionConfiguration = subscriptionConfigurationOptions.Value;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create subscription
        
        TopicName topicName = new TopicName(_subscriptionConfiguration.ProjectId, _subscriptionConfiguration.TopicId);
        SubscriptionName subscriptionName = new SubscriptionName(_subscriptionConfiguration.ProjectId, _subscriptionConfiguration.SubscriptionId);
        PushConfig? pushConfig = null;
        int ackDeadlineSeconds = 60;
        try
        {
            _logger.LogInformation("Creating subscription ...");
            _subscriberServiceApiClient.CreateSubscription(subscriptionName, topicName, pushConfig, ackDeadlineSeconds);
            _logger.LogInformation("Subscription created");
        }
        catch (Grpc.Core.RpcException)
        {
            _logger.LogInformation("Subscription already exists");
        }

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _subscriberClient.StartAsync((msg, cancellationToken) =>
        {
            Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
            Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");
            return Task.FromResult(SubscriberClient.Reply.Ack);
        });
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _subscriberClient.StopAsync(cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}