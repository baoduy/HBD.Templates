using HBD.EfCore.BizAction;
using Microsoft.AspNetCore.Mvc;
using TEMP.Api.Controllers.Abstractions;
using TEMP.AppServices.Features.Profiles.Actions;
using TEMP.AppServices.Features.Profiles.Models;
using TEMP.AppServices.Features.Profiles.Queries;
using TEMP.AppServices.Share;

namespace TEMP.Api.Controllers.V1;

[ApiVersion("1")]
public class ProfileController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileBasicView>> Get([FromQuery]PageableQueryModel query, [FromServices] IProfileQueryService repo)
    {
        var p = await repo.GetPages(query).ConfigureAwait(false);
        return this.Send(p);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProfileBasicView>> Get([FromRoute]Guid id, [FromServices] IProfileQueryService repo)
    {
        var p = await repo.GetById(id).ConfigureAwait(false);
        return this.Send(p);
    }
    
    [HttpPost]
    public async Task<ActionResult<ProfileBasicView>> Post([FromBody] CreateProfileModel model,
        [FromServices] IActionServiceAsync<ICreateProfileAction> action)
    {
        var result = await action.RunBizActionAsync<ProfileBasicView>(model).ConfigureAwait(false);
        return action.Status.Send(result);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProfileBasicView>> Put(Guid id, [FromBody] UpdateProfileModel model,
        [FromServices] IActionServiceAsync<IUpdateProfileAction> action)
    {
        var result = await action.RunBizActionAsync<ProfileBasicView>(model).ConfigureAwait(false);
        return action.Status.Send(result);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ProfileBasicView>> Delete(Guid id, [FromServices] IActionServiceAsync<IDeleteProfileAction> action)
    {
        await action.RunBizActionAsync(new ProfileDeleteModel{Id = id}).ConfigureAwait(false);
        return action.Status.Send();
    }
}