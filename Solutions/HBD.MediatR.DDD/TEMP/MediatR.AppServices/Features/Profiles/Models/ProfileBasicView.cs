using Profile = MediatR.Domains.Features.Profiles.Entities.Profile;
// ReSharper disable ClassNeverInstantiated.Global

namespace MediatR.AppServices.Features.Profiles.Models;

public record ProfileBasicView
{
    public Guid Id { get; set; } = default!;
    
    public string? Name { get; set; }
}