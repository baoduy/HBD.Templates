using AutoMapper;
using HBD.AutoMapper.Lazy;

namespace MediatR.AppServices.Share;

public abstract class BaseRequestHandler
{
    private readonly ILazyMapper _mapper;

    protected BaseRequestHandler(ILazyMapper mapper) => _mapper = mapper;

    public ILazyMap<TResult> LazyFor<TResult>(object originalValue) => _mapper.Map<TResult>(originalValue);
}