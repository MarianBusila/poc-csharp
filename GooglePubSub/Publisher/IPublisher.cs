namespace Publisher;

public interface IPublisher
{
    Task PublishMessage();
    Task CreateTopic(string group, string name);
}