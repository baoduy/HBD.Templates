using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.Features.Profiles.Actions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Features.Profiles.Queries;

namespace MediatR.Api.Controllers.V2;

[ApiVersion("2")]
public class ProfileControllerV2 : ApiControllerBase
{
    private readonly IMediator _mediator;
    public ProfileControllerV2(IMediator mediator) => _mediator = mediator;
    
    [HttpGet]
    public async Task<ProfileBasicView> Get(SingleProfileQuery query) => await _mediator.Send(query).ConfigureAwait(false);

    // POST api/<controller>
    [HttpPost]
    public async Task<ProfileBasicView> Post([FromBody] CreateProfileCommand model) => await _mediator.Send(model);
}