using System;

namespace MediatR.Domains.Features.Profiles.Entities
{
    public partial class ProfileDto
    {
        public string? Avatar { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Email { get; set; }
        public string MembershipNo { get; set; }
        public string Name { get; set; }
        public string? Phone { get; set; }
        public Guid? DataKey { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTimeOffset LastModifiedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTimeOffset? UpdatedOn { get; set; }
        public Guid Id { get; set; }
        public byte[]? RowVersion { get; set; }
    }
}