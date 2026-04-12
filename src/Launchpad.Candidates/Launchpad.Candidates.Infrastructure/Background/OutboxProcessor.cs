using Launchpad.Candidates.Infrastructure.MessageBroker.Kafka;
using Launchpad.Candidates.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Launchpad.Candidates.Infrastructure.Background;

/// <summary>
///     A background service class responsible for processing outbox messages in a distributed system.
///     This is typically used to ensure reliable message delivery by processing messages stored in an
///     outbox table.
/// </summary>
public class OutboxProcessor(ILogger<OutboxProcessor> logger, IServiceScopeFactory scopeFactory, IKafkaProducer producer, IEventTopicResolver eventTopicResolver) : BackgroundService
{
    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessOutboxMessages(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to process Outbox");
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500), stoppingToken);
        }
    }

    private async Task ProcessOutboxMessages(CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var messages = await dbContext.OutboxMessages
            .Where(m => m.ProcessedAt == null)
            .Take(25)
            .ToListAsync(ct);

        foreach (var message in messages)
        {
            try
            {
                var topic = eventTopicResolver.GetTopicName(message.Type);
                await producer.ProduceAsync(topic, message.Id.ToString(), message.Content, ct);

                message.CompleteProcessing();
            }
            catch (Exception ex)
            {
                message.FailProcessing(ex.Message);
                logger.LogError(ex, "Failed to send message {Id} to Kafka", message.Id);
            }
        }

        await dbContext.SaveChangesAsync(ct);
    }
}