using System.ComponentModel.DataAnnotations;
using TEMP.AppServices.Share;

namespace TEMP.AppServices.Features.Profiles.Models;

public class UpdateProfileModel: ModelBase
{
    [Required] public Guid Id { get; set; } = default!;

    [Phone] public string? Phone { get; set; }

    public string? Name { get; set; } = default!;
}