using Dapr.Actors.Runtime;

namespace ActorReminderPartitioning.Actors;

public class ReminderActor : Actor, IReminderActor, IRemindable
{
    private readonly HttpClient _client;
    
    private const string ReminderName = "reminder";
    
    public ReminderActor(
        IHttpClientFactory httpClientFactory,
        ActorHost host) 
        : base(host)
    {
        _client = httpClientFactory.CreateClient();
    }

    public async Task SetReminder()
    {
        await RegisterReminderAsync(
            ReminderName,
            null,
            TimeSpan.FromMinutes(5),
            TimeSpan.FromMilliseconds(-1));
    }

    public async Task<object> GetReminder()
    {
        var uri = new Uri("http://localhost:3570/v1.0/actors/ReminderActor/reminderactor/reminders/reminder");
        
        var response = await _client.SendAsync(new HttpRequestMessage(HttpMethod.Get, uri));
        var content = await response.Content.ReadAsStringAsync();

        return content;
    }

    public async Task DeleteReminder()
    {
        await UnregisterReminderAsync(ReminderName);
    }
    
    public async Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        Console.WriteLine("Reminder triggered");
    }
}