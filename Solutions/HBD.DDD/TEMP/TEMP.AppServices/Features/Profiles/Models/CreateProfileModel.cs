using System.ComponentModel.DataAnnotations;
using AutoMapper;
using TEMP.AppServices.Share;
using Profile = TEMP.Domains.Features.Profiles.Entities.Profile;

namespace TEMP.AppServices.Features.Profiles.Models;

[AutoMap(typeof(Profile), ReverseMap = true)]
public class CreateProfileModel : ModelBase
{
    [Required] public string Email { get; set; } = default!;

    [Phone] public string Phone { get; set; } = default!;

    internal string MembershipNo { get; set; } = default!;

    [StringLength(150)] [Required] public string Name { get; set; } = default!;
}