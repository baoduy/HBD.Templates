using HBD.EfCore.BizAction;
using Microsoft.AspNetCore.Mvc;
using TEMP.Api.Controllers.Abstractions;
using TEMP.AppServices.BizActions.Profiles;
using TEMP.AppServices.Models.Profiles;
using TEMP.AppServices.QueryServices;

namespace TEMP.Api.Controllers.V2;

[ApiVersion("2")]
public class ProfileControllerV2 : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ProfileBasicView>> Get([FromServices] IProfileQueryService repo)
    {
        var p = await repo.GetBasicViewForUserAsync(Guid.Empty).ConfigureAwait(false);
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