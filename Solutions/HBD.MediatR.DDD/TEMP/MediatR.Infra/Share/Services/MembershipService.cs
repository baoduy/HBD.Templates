using MediatR.Domains.Services;
using MediatR.Domains.Share;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Infra.Share.Services;

internal sealed class MembershipService : SequenceService, IMembershipService
{
    public MembershipService(DbContext dbContext) : base(dbContext, Sequences.Membership)
    {
    }
}