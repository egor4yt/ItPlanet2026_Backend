using Launchpad.Candidates.Domain.Events;

namespace Launchpad.Candidates.Infrastructure.MessageBroker.Kafka;

public class EventTopicResolver : IEventTopicResolver
{
    private readonly Dictionary<string, string> _topicMap = new Dictionary<string, string>
    {
        { nameof(SkillCreated), "skill-created" }
    };


    public string GetTopicName(string eventType)
    {
        return _topicMap.TryGetValue(eventType, out var topic)
            ? topic
            : throw new ArgumentException($"No one topic is configured for {eventType}");
    }
}