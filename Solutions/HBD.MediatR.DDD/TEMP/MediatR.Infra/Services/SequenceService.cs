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

    public virtual ValueTask<string> NextValueAsync() => _dbContext.NextSeqValueWithFormat(_sequence);


    private readonly DbContext _dbContext;
    private readonly Sequences _sequence;
}