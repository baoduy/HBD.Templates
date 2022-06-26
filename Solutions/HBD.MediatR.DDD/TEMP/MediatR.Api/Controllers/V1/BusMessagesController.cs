using MediatR.Api.Controllers.Abstractions;
using MediatR.AppServices.Features.Bus.Models;
using MediatR.AppServices.Features.Profiles.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR.Infra.ServiceBus.Senders;

namespace MediatR.Api.Controllers.V1;

[ApiVersion("1")]
public class BusMessagesController: ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProfileBasicView>> Post([FromBody] BusMessageModel model,
        [FromServices] ITopic1Sender sender)
    {
        await sender.SendAsync(model).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpPost("Bulk")]
    public async Task<ActionResult<ProfileBasicView>> BulkPost([FromBody] BusMessageModel model,
        [FromServices] ITopic1Sender sender)
    {
        var messages = Enumerable.Range(1, 10).Select(i => new BusMessageModel { Message = $"{model.Message} - {i}" }).ToArray();
        
        await sender.SendAsync(messages).ConfigureAwait(false);
        return Ok();
    }
}