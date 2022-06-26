using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using MediatR.Domains.Share;

namespace MediatR.Domains.Features.Profiles.Entities;

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
        Phone = phone;
        MembershipNo = memberShipNo;
        UpdateName(name, userId);
    }

    private Profile()
    {
    }

    [MaxLength(50)] public string Avatar { get; private set; }
    
    [Column(TypeName = "Date")] public DateTime? BirthDay { get; private set; }

    [MaxLength(150)]
    [EmailAddress]
    [Required]
    public string Email { get; }

    [MaxLength(50)] [Required] public string MembershipNo { get; }

    [MaxLength(150)] [Required] 
    public string Name { get; private set; }

    [Phone] [MaxLength(50)] public string Phone { get; }

    public void Update(string avatar, DateTime? birthday, string userId)
    {
        Avatar = avatar;
        BirthDay = birthday;
        SetUpdatedBy(userId);
    }

    public void UpdateName(string name, string userId)
    {
        Name = name;
        SetUpdatedBy(userId);
    }
}