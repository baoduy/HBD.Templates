using MediatR.Api.Controllers.Abstractions;
using MediatR.AppServices.Features.Bus.Models;
using MediatR.AppServices.Features.Profiles.Models;
using Microsoft.AspNetCore.Mvc;
using SlimMessageBus;

namespace MediatR.Api.Controllers.V1;

[ApiVersion("1")]
public class BusMessagesController: ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProfileBasicView>> Post([FromBody] BusMessageModel model,
        [FromServices] IMessageBus sender)
    {
        await sender.Publish(model).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpPost("Bulk")]
    public async Task<ActionResult<ProfileBasicView>> BulkPost([FromBody] BusMessageModel model,
        [FromServices] IMessageBus sender)
    {
        foreach (var message in Enumerable.Range(1, 10).Select(i => new BusMessageModel { Message = $"{model.Message} - {i}" }))
            await sender.Publish(message).ConfigureAwait(false);

        return Ok();
    }
}