using HBD.EfCore.Abstractions.Pageable;
using HBD.MediatR.EfAutoSave.AutoMappers;
using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.Features.Profiles.Actions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Features.Profiles.Queries;
using MediatR.AppServices.Share.Exceptions;

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
    public async Task<ProfileBasicView?> Create([FromBody] CreateProfileCommand model)
        => await _mediator.Send(model);

    [HttpPut("{id:guid}")]
    public async Task<ProfileBasicView?> Update(Guid id, [FromBody] UpdateProfileCommand model)
    {
        if (model.Id != id) 
            throw new BizCommandException($"The Id {id} is invalid.", nameof(UpdateProfileCommand.Id));

        return await _mediator.Send(model);
    }

    [HttpDelete("{id:guid}")]
    public async Task Delete([FromRoute] DeleteProfileCommand model) => await _mediator.Send(model);
}