using System.Text.Json;
using Launchpad.Candidates.Domain.Common;
using Launchpad.Candidates.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Launchpad.Candidates.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Skill> Skills { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var domainEntities = ChangeTracker.Entries<IHasDomainEvents>()
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.DomainEvents)
            .ToList();

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var domainEvent in domainEvents)
        {
            var message = new OutboxMessage(domainEvent.GetType().Name, JsonSerializer.Serialize(domainEvent, domainEvent.GetType()));
            OutboxMessages.Add(message);
        }

        var result = await base.SaveChangesAsync(ct);

        foreach (var entity in ChangeTracker.Entries<IHasDomainEvents>())
        {
            entity.Entity.ClearDomainEvents();
        }

        return result;
    }
}