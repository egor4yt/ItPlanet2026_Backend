namespace Launchpad.Candidates.Infrastructure.MessageBroker.Kafka;

public interface IEventTopicResolver
{
    string GetTopicName(string eventType);
}