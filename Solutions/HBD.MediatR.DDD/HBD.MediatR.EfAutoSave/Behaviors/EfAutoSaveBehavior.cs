using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HBD.MediatR.EfAutoSave.Behaviors;

internal sealed class EfAutoSaveBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly EfAutoSaveOptions _options;
    private readonly IServiceProvider _serviceProvider;

    public EfAutoSaveBehavior(EfAutoSaveOptions options, IServiceProvider serviceProvider)
    {
        _options = options;
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var rp = await next();
        if (rp is null) return rp;
        
        //Call DbContext Save Changes
        var dbContext = _serviceProvider.GetService(_options.DbContextType) as DbContext ??
                        _serviceProvider.GetService<DbContext>();

        if (dbContext != null)
            await dbContext.SaveChangesAsync(cancellationToken);

        return rp;
    }
}