using AutoMapper;

// ReSharper disable PublicConstructorInAbstractClass

namespace MediatR.AppServices.QueryServices;

public interface IQueryService
{
}

internal abstract class QueryService
{
    protected IMapper Mapper { get; }

    public QueryService(IMapper mapper) => Mapper = mapper;
}