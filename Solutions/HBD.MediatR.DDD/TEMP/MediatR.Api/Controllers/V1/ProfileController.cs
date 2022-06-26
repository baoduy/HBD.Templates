using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.Features.Profiles.Actions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Features.Profiles.Queries;

namespace MediatR.Api.Controllers.V1;

[ApiVersion("1")]
public class ProfileController : ApiControllerBase
{
    private readonly IMediator _mediator;
    public ProfileController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<ProfileBasicView>> Get([FromServices] IProfileQueryService repo)
    {
        var p = await repo.GetBasicViewAsync().ConfigureAwait(false);
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