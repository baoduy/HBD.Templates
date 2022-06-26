using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TEMP.Domains.Share;

namespace TEMP.Domains.Features.Profiles.Entities;

[Table("Profiles", Schema = DomainSchemas.Profile)]
public class Profile : AggregateRoot
{
    public Profile([NotNull] string name, string memberShipNo, string email, string phone,
        string userId)
        : this(default, name, memberShipNo, email, phone, userId)
    {
    }

    public Profile(Guid id, [NotNull] string name, string memberShipNo, string email,
        string phone,
        string userId)
        : base(id, userId)
    {
        Email = email;
        MembershipNo = memberShipNo;
        
        Update(null, name, phone,null,userId);
    }

    private Profile()
    {
    }

    [MaxLength(50)] public string? Avatar { get; private set; }
    
    [Column(TypeName = "Date")] public DateTime? BirthDay { get; private set; }

    [MaxLength(150)]
    [EmailAddress]
    [Required]
    public string Email { get;private set; } = default!;

    [MaxLength(50)] [Required] public string MembershipNo { get; private set; } = default!;

    [MaxLength(150)] [Required] 
    public string Name { get; private set; } = default!;

    [Phone] [MaxLength(50)] public string? Phone { get; private set; }

    public void Update(string? avatar,string? name, string? phoneNumber, DateTime? birthday, string userId)
    {
        Avatar = avatar;
        BirthDay = birthday;

        if (!string.IsNullOrEmpty(name))
            Name = name;
        if (!string.IsNullOrEmpty(phoneNumber))
            Phone = phoneNumber;
        
        SetUpdatedBy(userId);
    }
}