using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Infra;

public sealed class BoundedContextBuilder
{
    #region Constructors

    internal BoundedContextBuilder(IServiceCollection service, Assembly[] assembliesToScans)
    {
        _service = service;
        _assembliesToScans = assembliesToScans;
    }

    #endregion Constructors

    #region Fields

    private readonly Assembly[] _assembliesToScans;
    private readonly IServiceCollection _service;

    #endregion Fields

    #region Methods

    public BoundedContextBuilder AddMoreContext<TContext>(Action<DbContextOptionsBuilder> contextBuilder,
        Expression<Func<Type, bool>> entityFilter = null,
        ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
        ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : DbContext
    {
        _service.AddBoundedContext<TContext>(contextBuilder,
            _assembliesToScans,
            entityFilter,
            contextLifetime,
            optionsLifetime);

        return this;
    }

    public ServiceProvider BuildServiceProvider()
    {
        return _service.BuildServiceProvider();
    }

    #endregion Methods
}