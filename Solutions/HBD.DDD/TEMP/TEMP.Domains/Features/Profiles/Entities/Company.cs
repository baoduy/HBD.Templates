using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TEMP.Domains.Features.Profiles.Entities;

[Owned]
public class Company
{
    public Company(string name, string uen, string abn, string arbn, string can)
    {
        Name = name;
        UEN = uen;
        ABN = abn;
        ARBN = arbn;
        CAN = can;
    }

    internal Company()
    {
    }

    [MaxLength(50)] public string? ABN { get; private set; }

    [MaxLength(50)] public string? ARBN { get; private set; }

    [MaxLength(50)] public string? CAN { get; private set; }

    [MaxLength(100),Required] public string Name { get; private set; } = default!;

    [MaxLength(100),Required] public string UEN { get; private set; } = default!;
    
}