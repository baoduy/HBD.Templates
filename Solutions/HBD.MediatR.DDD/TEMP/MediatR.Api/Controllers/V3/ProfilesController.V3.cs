using HBD.MediatR.EfAutoSave.AutoMappers;
using MediatR.Api.Controllers.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MediatR.AppServices.Features.Profiles.Actions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.AppServices.Features.Profiles.Queries;

namespace MediatR.Api.Controllers.V3;

[ApiVersion("3")]
public class ProfilesController : ApiControllerBase
{
    private readonly IAutoMapMediator _mediator;
    public ProfilesController(IAutoMapMediator mediator) => _mediator = mediator;
    
    [HttpGet]
    public async Task<ProfileBasicView?> Get(SingleProfileQuery query) => await _mediator.Send(query).ConfigureAwait(false);
    
    [HttpPost]
    public async Task<ProfileBasicView?> Post([FromBody] CreateProfileCommandV3 model) => await _mediator.Send<ProfileBasicView>(model);
}