using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TEMP.Domains.Abstracts;

namespace TEMP.Domains.Aggregators;

[Table("Profiles", Schema = DomainSchemas.Profile)]
public class Profile : AggregateRoot
{
    public Profile([NotNull] string name, string memberShipNo, Guid adAccountId, string email, string phone,
        string userId)
        : this(default, name, memberShipNo, adAccountId, email, phone, userId)
    {
    }

    public Profile(Guid id, [NotNull] string name, string memberShipNo, Guid adAccountId, string email,
        string phone,
        string userId)
        : base(id, userId)
    {
        Email = email;
        Phone = phone;
        MembershipNo = memberShipNo;
        AdAccountId = adAccountId;

        UpdateName(name, userId);
    }

    private Profile()
    {
    }

    public Guid AdAccountId { get; }
    
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