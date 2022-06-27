using AutoMapper;

namespace MediatR.AppServices.Share;

public abstract class BaseRequestHandler
{
    private readonly IMapper _mapper;

    protected BaseRequestHandler(IMapper mapper) => _mapper = mapper;

    public ILazy<TResult> LazyFor<TResult>(object originalValue) => new LazyResult<TResult>(_mapper, originalValue);
}