using Dapr.Actors;

namespace ActorReminderPartitioning.Actors;

public interface IReminderActor : IActor
{
    Task SetReminder();

    Task<object> GetReminder();
    
    Task DeleteReminder();
}