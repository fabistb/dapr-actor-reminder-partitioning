using ActorReminderPartitioning.Actors;
using ActorReminderPartitioning.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace ActorReminderPartitioning.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActorController : ControllerBase
{
    private readonly IActorFactory<IReminderActor> _reminderActor;

    private const string ActorId = "reminderactor";

    public ActorController(IActorFactory<IReminderActor> reminderActor)
    {
        _reminderActor = reminderActor;
    }
    
    [HttpPost]
    public async Task<IActionResult> SetReminder()
    {
        var actor = _reminderActor.CreateActor(ActorId);
        await actor.SetReminder();

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetReminder()
    {
        var actor = _reminderActor.CreateActor(ActorId);
        var result = await actor.GetReminder();
        
        return new OkObjectResult(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteReminder()
    {
        var actor = _reminderActor.CreateActor(ActorId);
        await actor.DeleteReminder();

        return Ok();
    }
}