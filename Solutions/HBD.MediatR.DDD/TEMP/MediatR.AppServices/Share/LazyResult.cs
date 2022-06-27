using AutoMapper;

namespace MediatR.AppServices.Share;

public interface ILazy<out TResult>
{
    public TResult Value { get; }
}

internal sealed class LazyResult<TResult> :ILazy<TResult>
{
    private readonly IMapper _mapper;
    private readonly object _originalValue;

    public LazyResult(IMapper mapper,object originalValue)
    {
        _mapper = mapper;
        _originalValue = originalValue;
    }

    public TResult Value => _mapper.Map<TResult>(_originalValue);
}