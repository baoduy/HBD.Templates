using Microsoft.EntityFrameworkCore;
using TEMP.Domains;
using TEMP.Domains.Services;

namespace TEMP.Infras.Services
{
    internal sealed class MembershipService : SequenceService, IMembershipService
    {
        public MembershipService(DbContext dbContext) : base(dbContext, Sequences.Membership)
        {
        }
    }
}