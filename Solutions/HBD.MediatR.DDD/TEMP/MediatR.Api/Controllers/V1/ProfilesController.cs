using HBDStack.EfCore.Abstractions.Pageable;
using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.Features.Profiles.Actions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Features.Profiles.Queries;

namespace MediatR.Api.Controllers.V1;

[ApiVersion("1")]
public class ProfilesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    public ProfilesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IPageable<ProfileBasicView>?> Get([FromQuery] PageProfileQuery query)
        => await _mediator.Send(query).ConfigureAwait(false);

    [HttpGet("{id:guid}")]
    public async Task<ProfileBasicView?> GetById([FromRoute] Guid id)
        => await _mediator.Send(new SingleProfileQuery { Id = id }).ConfigureAwait(false);

    [HttpPost]
    public async Task<ActionResult<ProfileBasicView?>> Create([FromBody] CreateProfileCommand model)
    {
        var rs = await _mediator.Send(model);
        return rs.Send();
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ProfileBasicView?>> Update(Guid id, [FromBody] UpdateProfileCommand model)
    {
        var rs= await _mediator.Send(model);
        return rs.Send();
    }

    [HttpDelete("{id:guid}")]
    public async Task Delete([FromRoute] DeleteProfileCommand model) => await _mediator.Send(model);
}