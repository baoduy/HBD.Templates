using Microsoft.EntityFrameworkCore;
using MediatR.Domains;
using MediatR.Domains.Services;

namespace MediatR.Infra.Services;

internal abstract class SequenceService : ISequenceServices
{
    protected SequenceService(DbContext dbContext, Sequences sequence)
    {
        _dbContext = dbContext;
        _sequence = sequence;
    }

    public virtual async ValueTask<string> NextValueAsync() =>
        _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.SqlServer"
            ? await _dbContext.NextSeqValueWithFormat(_sequence)
            : Guid.NewGuid().ToString();


    private readonly DbContext _dbContext;
    private readonly Sequences _sequence;
}