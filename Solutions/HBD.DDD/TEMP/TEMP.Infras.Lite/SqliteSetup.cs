using System;
using System.Threading.Tasks;
using HBD.EfCore.Events.Handlers;
using TEMP.Domains.Events;
using TEMP.Infras;
using TEMP.Infras.EventHandlers;
using TEMP.Infras.Lite;
// ReSharper disable CheckNamespace

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqliteSetup
    {
        #region Methods

        public static IServiceCollection AddInfraLite(this IServiceCollection service)
        {
            service
                .AddGenericRepositories<TEMPContext>()
                .AddImplementations()
                .AddSingleton<IBeforeSaveEventHandlerAsync<ProfileCreatedEvent>,ProfileCreatedHandler>()
                //.AddSingleton< IBeforeSaveEventHandler<ProfileCreatedEvent>,ProfileCreatedHandler>()
                .AddBoundedContext<TEMPContext>(op => op.UseSqliteMemory());
            return service;
        }

        public static async Task EnsureDbCreatedAsync(this IServiceProvider serviceProvider) 
            => await serviceProvider.GetRequiredService<TEMPContext>().Database.EnsureCreatedAsync().ConfigureAwait(false);

        public static async Task EnsureDbDeletedAsync(this IServiceProvider serviceProvider) 
            => await serviceProvider.GetRequiredService<TEMPContext>().Database.EnsureDeletedAsync().ConfigureAwait(false);

        #endregion Methods
    }
}