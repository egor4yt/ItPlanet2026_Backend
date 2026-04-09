namespace Launchpad.Candidates.Infrastructure.MessageBroker.Kafka;

public interface IKafkaProducer
{
    Task ProduceAsync(string topic, string key, string value, CancellationToken ct = default);
}