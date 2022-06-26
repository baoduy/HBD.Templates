using System.Diagnostics.CodeAnalysis;
using HBD.EfCore.DataAuthorization;
using MediatR.Domains.Features.Profiles.Entities;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Infra;

internal class TEMPContext : DbContext, IDataKeyDbContext
{
    //Internal fields will be available in unit test project.
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    internal readonly IDataKeyProvider _dataKeyProvider;

    #region Constructors

    /// <summary>
    /// </summary>
    /// <param name="options"></param>
    /// <param name="dataKeyProviders">
    ///     optional <see cref="IDataKeyProvider" /> injected from DI. Only first runner will be picked.
    /// </param>
    public TEMPContext(DbContextOptions options,
        [AllowNull] IEnumerable<IDataKeyProvider> dataKeyProviders)
        : base(options)
    {
        //Ensure only 1 Data Key Provider is registered.
        _dataKeyProvider = dataKeyProviders?.SingleOrDefault();
    }

    #endregion Constructors

    public IEnumerable<Guid> ImpersonateKeys => _dataKeyProvider?.GetImpersonateKeys() ?? Enumerable.Empty<Guid>();

    public Guid OwnershipKey => _dataKeyProvider?.GetOwnershipKey() ?? Guid.Empty;

    #region Properties

    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public virtual DbSet<Profile> Profiles { get; private set; }

    #endregion Properties
}