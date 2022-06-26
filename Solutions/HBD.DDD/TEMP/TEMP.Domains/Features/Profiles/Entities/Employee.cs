using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TEMP.Domains.Share;

namespace TEMP.Domains.Features.Profiles.Entities;

public enum EmployeeType
{
    Director = 1,
    Secretary = 2,
    Other = 3
}

[Table("Employees", Schema = DomainSchemas.Profile)]
public class Employee : DomainEntity
{
    public Employee(Guid profileId, EmployeeType type, string userId) : base(Guid.NewGuid(), userId)
    {
        ProfileId = profileId;
        PromoteTo(type, userId);
    }

    private Employee()
    {
    }


    [Required] public virtual Profile Profile { get; private set; } = default!;

    [Required] public Guid ProfileId { get; private set; }

    [Required] public EmployeeType Type { get; private set; }
    

    public void PromoteTo(EmployeeType type, string userId)
    {
        Type = type;
        SetUpdatedBy(userId);
    }
}