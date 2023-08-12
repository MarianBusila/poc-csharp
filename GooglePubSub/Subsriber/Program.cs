using Google.Cloud.PubSub.V1;

string projectId = "unity-solutions-pwest-test";
string topicId = "test-marian";
string subscriptionId = "test-marian-subscription";

// Create subscription
SubscriberServiceApiClient subscriberServiceApiClient = await SubscriberServiceApiClient.CreateAsync();
TopicName topicName = new TopicName(projectId, topicId);
SubscriptionName subscriptionName = new SubscriptionName(projectId, subscriptionId);
PushConfig? pushConfig = null;
int ackDeadlineSeconds = 60;
try
{
    Console.WriteLine("Creating subscription ...");
    subscriberServiceApiClient.CreateSubscription(subscriptionName, topicName, pushConfig, ackDeadlineSeconds);
    Console.WriteLine("Subscription created");
}
catch (Grpc.Core.RpcException)
{
    Console.WriteLine("Subscription already exists");
}

// Pull messages from the subscription
SubscriberClient subscriber = await SubscriberClient.CreateAsync(subscriptionName);

await subscriber.StartAsync((msg, cancellationToken) =>
{
    Console.WriteLine($"Received message {msg.MessageId} published at {msg.PublishTime.ToDateTime()}");
    Console.WriteLine($"Text: '{msg.Data.ToStringUtf8()}'");
    return Task.FromResult(SubscriberClient.Reply.Ack);
});

await subscriber.StopAsync(TimeSpan.FromSeconds(15));