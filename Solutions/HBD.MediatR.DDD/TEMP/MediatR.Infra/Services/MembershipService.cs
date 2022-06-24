using Microsoft.EntityFrameworkCore;
using MediatR.Domains;
using MediatR.Domains.Services;

namespace MediatR.Infra.Services;

internal sealed class MembershipService : SequenceService, IMembershipService
{
    public MembershipService(DbContext dbContext) : base(dbContext, Sequences.Membership)
    {
    }
}