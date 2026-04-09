using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Launchpad.Candidates.Infrastructure.MessageBroker.Kafka;

public partial class KafkaProducer(ILogger<KafkaProducer> logger, IProducer<string, string> producer) : IKafkaProducer
{
    public async Task ProduceAsync(string topic, string key, string value, CancellationToken ct = default)
    {
        var message = new Message<string, string>
        {
            Key = key,
            Value = value
        };

        var result = await producer.ProduceAsync(topic, message, ct);
        LogDeliveredToTopic(result.TopicPartitionOffset);
    }

    [LoggerMessage(LogLevel.Debug, "Delivered to: {TopicPartitionOffset}")]
    partial void LogDeliveredToTopic(TopicPartitionOffset topicPartitionOffset);
}