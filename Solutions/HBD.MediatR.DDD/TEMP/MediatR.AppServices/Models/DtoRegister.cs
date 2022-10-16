using Mapster;
using MediatR.AppServices.Features.Profiles.Actions;
using MediatR.AppServices.Features.Profiles.Models;
using MediatR.Domains.Features.Profiles.Entities;

namespace MediatR.AppServices.Models;

internal class DtoRegister:IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Profile, ProfileBasicView>();
        config.ForType<Profile, ProfileDto>();
        
        config.ForType<CreateProfileCommand, Profile>()
            .ConstructUsing(c=>new Profile(c.Name,c.MembershipNo,c.Email,c.Phone,c.UserId));
    }
}