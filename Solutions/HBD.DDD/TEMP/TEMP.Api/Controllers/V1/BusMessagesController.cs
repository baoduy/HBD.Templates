using Microsoft.AspNetCore.Mvc;
using SlimMessageBus;
using TEMP.Api.Controllers.Abstractions;
using TEMP.AppServices.Features.Bus.Models;
using TEMP.AppServices.Features.Profiles.Models;

namespace TEMP.Api.Controllers.V1;

[ApiVersion("1")]
public class BusMessagesController: ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProfileBasicView>> Post([FromBody] MessageModel model,
        [FromServices] IMessageBus bus)
    {
        await bus.Publish(model).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpPost("Bulk")]
    public async Task<ActionResult<ProfileBasicView>> BulkPost([FromBody] MessageModel model,
        [FromServices] IMessageBus bus)
    {
        foreach (var message in Enumerable.Range(1, 10).Select(i => new MessageModel { Message = $"{model.Message} - {i}" }))
            await bus.Publish(message).ConfigureAwait(false);
        return Ok();
    }
}