using AutoMapper;

// ReSharper disable PublicConstructorInAbstractClass

namespace TEMP.AppServices.Features.Profiles.Queries;

public interface IQueryService
{
}

internal abstract class QueryService
{
    protected IMapper Mapper { get; }

    public QueryService(IMapper mapper) => Mapper = mapper;
}