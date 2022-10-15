using System;
using MediatR.Domains.Features.Profiles.Entities;

namespace MediatR.Domains.Features.Profiles.Entities
{
    public partial class EmployeeDto
    {
        public ProfileDto Profile { get; set; }
        public Guid ProfileId { get; set; }
        public EmployeeType Type { get; set; }
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