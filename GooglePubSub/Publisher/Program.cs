using Google.Cloud.PubSub.V1;

string projectId = "unity-solutions-pwest-test";
string topicId = "test-marian";

// Create topic
PublisherServiceApiClient publisherServiceApiClient = await PublisherServiceApiClient.CreateAsync();
TopicName topicName = new TopicName(projectId, topicId);

try {
    Console.WriteLine("Creating topic ...");
    publisherServiceApiClient.CreateTopic(topicName);
    Console.WriteLine("Topic created");
}
catch (Grpc.Core.RpcException)
{
    Console.WriteLine("Topic already exists");
}

// Publish a message to the topic
PublisherClient publisher = await PublisherClient.CreateAsync(topicName);
string messageId = await publisher.PublishAsync("Hello at " + DateTime.Now);
Console.WriteLine("Published MessageId: " + messageId);
await publisher.ShutdownAsync(TimeSpan.FromSeconds(5));