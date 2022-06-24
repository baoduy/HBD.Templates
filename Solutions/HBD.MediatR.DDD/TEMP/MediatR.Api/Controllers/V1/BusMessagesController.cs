using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.Models.Profiles;
using MediatR.Infra.ServiceBus.Senders;

namespace MediatR.Api.Controllers.V1;

[ApiVersion("1")]
public class BusMessagesController: ApiControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ProfileBasicView>> Post([FromBody] MessageModel model,
        [FromServices] ITopic1Sender sender)
    {
        await sender.SendAsync(model).ConfigureAwait(false);
        return Ok();
    }
    
    [HttpPost("Bulk")]
    public async Task<ActionResult<ProfileBasicView>> BulkPost([FromBody] MessageModel model,
        [FromServices] ITopic1Sender sender)
    {
        var messages = Enumerable.Range(1, 10).Select(i => new MessageModel { Message = $"{model.Message} - {i}" }).ToArray();
        
        await sender.SendAsync(messages).ConfigureAwait(false);
        return Ok();
    }
}