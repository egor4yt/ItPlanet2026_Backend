using Confluent.Kafka;
using Launchpad.Candidates.Infrastructure.Background;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Launchpad.Candidates.Infrastructure.MessageBroker.Kafka.Configuration;

internal static class DependencyInjection
{
    internal static void ConfigureMessageBrokers(this IHostApplicationBuilder app)
    {
        var kafkaConnectionString = app.Configuration.GetSection(Shared.ConfigurationKeys.KafkaConnectionString);
        if (string.IsNullOrWhiteSpace(kafkaConnectionString.Value))
        {
            Log.Warning("Kafka connection string is missing in the configuration file");
            return;
        }

        app.Services.AddSingleton<IProducer<string, string>>(_ =>
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = kafkaConnectionString.Value,
                    Acks = Acks.All,
                    MessageSendMaxRetries = 3
                };
                var builder = new ProducerBuilder<string, string>(config);
                return builder.Build();
            }
        );

        app.Services.AddSingleton<IEventTopicResolver, EventTopicResolver>();
        app.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
        app.Services.AddHostedService<OutboxProcessor>();
    }
}