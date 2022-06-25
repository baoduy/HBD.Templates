using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.BizActions.Profiles;
using MediatR.AppServices.Models.Profiles;
using MediatR.AppServices.QueryServices;

namespace MediatR.Api.Controllers.V2;

[ApiVersion("2")]
public class ProfileControllerV2 : ApiControllerBase
{
    private readonly IMediator _mediator;
    public ProfileControllerV2(IMediator mediator) => _mediator = mediator;
    
    [HttpGet]
    public async Task<ActionResult<ProfileBasicView>> Get([FromServices] IProfileQueryService repo)
    {
        var p = await repo.GetBasicViewForUserAsync(Guid.Empty).ConfigureAwait(false);
        return this.Send(p);
    }

    // POST api/<controller>
    [HttpPost]
    public async Task<ActionResult<ProfileBasicView>> Post([FromBody] CreateProfileCommand model)
    {
        var rs =await _mediator.Send(model);
        return Ok(rs);
    }

    // PUT api/<controller>/5
    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProfileBasicView>> Put(Guid id, [FromBody] UpdateProfileCommand model)
    {
        if (model.Id != id) return BadRequest();
        
        var rs =await _mediator.Send(model);
        return Ok(rs);
    }
}