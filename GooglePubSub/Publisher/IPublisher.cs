namespace Publisher;

public interface IPublisher
{
    Task PublishMessage();
    Task CreateTopic();
}