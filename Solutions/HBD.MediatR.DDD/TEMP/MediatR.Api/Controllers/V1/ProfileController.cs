using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.BizActions.Profiles;
using MediatR.AppServices.Models.Profiles;
using MediatR.AppServices.QueryServices;

namespace MediatR.Api.Controllers.V1;

[ApiVersion("1")]
public class ProfileController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileBasicView>> Get([FromServices] IProfileQueryService repo)
    {
        var p = await repo.GetBasicViewAsync().ConfigureAwait(false);
        return this.Send(p);
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<ActionResult<ProfileBasicView>> Post([FromBody] ProfileModel model,
        [FromServices] IActionServiceAsync<ICreateProfileAction> action)
    {
        var result = await action.RunBizActionAsync<ProfileBasicView>(model).ConfigureAwait(false);
        return action.Status.Send(result);
    }

    // PUT api/<controller>/5
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProfileBasicView>> Put(Guid id, [FromBody] ProfileModel model,
        [FromServices] IActionServiceAsync<IUpdateProfileAction> action)
    {
        var result = await action.RunBizActionAsync<ProfileBasicView>(model).ConfigureAwait(false);
        return action.Status.Send(result);
    }
    
}