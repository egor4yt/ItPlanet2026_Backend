namespace Launchpad.Warehouse.Infrastructure.MessageBroker.Kafka;

public interface IEventTopicResolver
{
    string GetTopicName(string eventType);
}