using ActorReminderPartitioning.Actors;
using ActorReminderPartitioning.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddDaprClient();
builder.Services.AddControllers().AddDapr();
builder.Services.AddHealthChecks();

builder.Services.AddTransient<IActorFactory<IReminderActor>, ActorFactory<IReminderActor>>();

builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<ReminderActor>();
    //options.RemindersStoragePartitions = 5;
});

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDaprSidekick(builder.Configuration);
}

var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();

app.UseEndpoints(enndpoints =>
{
    enndpoints.MapHealthChecks("/health");
    enndpoints.MapControllers();
    enndpoints.MapActorsHandlers();
});

app.Run();