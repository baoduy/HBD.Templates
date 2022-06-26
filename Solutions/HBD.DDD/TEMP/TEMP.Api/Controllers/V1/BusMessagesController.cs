using Microsoft.AspNetCore.Mvc;
using TEMP.Api.Controllers.Abstractions;
using TEMP.AppServices.Features.Bus.Models;
using TEMP.AppServices.Features.Profiles.Models;
using TEMP.Infra.ServiceBus.Senders;

namespace TEMP.Api.Controllers.V1;

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