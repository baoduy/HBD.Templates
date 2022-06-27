using HBD.MediatR.EfAutoSave.AutoMappers;
using HBD.MediatR.EfAutoSave.Behaviors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HBD.MediatR.EfAutoSave;

public static class EfAutoSaveSetup
{
    public static IServiceCollection AddEfAutoSaveBehavior<TDbContext>(this IServiceCollection serviceCollection)where TDbContext:DbContext =>
        serviceCollection.AddSingleton(new EfAutoSaveOptions { DbContextType = typeof(TDbContext) })
            .AddScoped<IAutoMapMediator,AutoMapMediator>()
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(EfAutoSaveBehavior<,>));
}