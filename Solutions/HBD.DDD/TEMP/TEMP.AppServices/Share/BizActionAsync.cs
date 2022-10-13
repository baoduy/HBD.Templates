using AutoMapper;
using HBD.EfCore.BizAction;
using HBDStack.EfCore.Repos.Abstractions;
using TEMP.Domains.Share;

// ReSharper disable MemberCanBePrivate.Global

namespace TEMP.AppServices.Share;

internal abstract class BizActionAsync : BizActionStatus
{
    protected IMapper Mapper { get; }
    protected IPrincipalProvider PrincipalProvider { get; }
        
    protected BizActionAsync(IPrincipalProvider principalProvider, IMapper mapper)
    {
        PrincipalProvider = principalProvider;
        Mapper = mapper;
    }
}

internal abstract class BizActionAsync<TEntity> : BizActionAsync where TEntity : AggregateRoot
{
    protected IRepository<TEntity> Repository { get; }

    protected BizActionAsync(IPrincipalProvider principalProvider, IMapper mapper, IRepository<TEntity> repository) : base(principalProvider, mapper) => Repository = repository;
}