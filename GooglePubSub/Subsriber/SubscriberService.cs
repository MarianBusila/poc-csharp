using Google.Cloud.PubSub.V1;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Subsriber;

public class SubscriberService : BackgroundService
{
    private readonly SubscriberClient _subscriberClient;
    private readonly SubscriberServiceApiClient _subscriberServiceApiClient;
    private readonly ILogger<SubscriberService> _logger;

    public SubscriberService(SubscriberClient subscriberClient, ILogger<SubscriberService> logger, SubscriberServiceApiClient subscriberServiceApiClient)
    {
        _subscriberClient = subscriberClient;
        _logger = logger;
        _subscriberServiceApiClient = subscriberServiceApiClient;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        // Create subscription
        string projectId = "unity-solutions-pwest-test";
        string topicId = "test-marian";
        string subscriptionId = "test-marian-subscription";
        
        TopicName topicName = new TopicName(projectId, topicId);
        SubscriptionName subscriptionName = new SubscriptionName(projectId, subscriptionId);
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