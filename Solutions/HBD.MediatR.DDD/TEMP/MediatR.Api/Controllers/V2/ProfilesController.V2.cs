using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Features.Profiles.Queries;

namespace MediatR.Api.Controllers.V2;

[ApiVersion("2")]
public class ProfilesController : ApiControllerBase
{
    private readonly IMediator _mediator;
    public ProfilesController(IMediator mediator) => _mediator = mediator;
    
    [HttpGet]
    public async Task<ProfileBasicView?> Get(SingleProfileQuery query) => await _mediator.Send(query).ConfigureAwait(false);
}