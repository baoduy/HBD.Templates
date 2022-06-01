using System;
using System.ComponentModel.DataAnnotations.Schema;
using TEMP.Domains.Abstracts;
using TEMP.Domains.Aggregators;

namespace TEMP.Domains.Entities;

public enum EmployeeType
{
    Director = 1,
    Secretary = 2,
    Other = 3
}

[Table("Employees", Schema = DomainSchemas.Profile)]
public class Employee : DomainEntity
{
    #region Constructors

    public Employee(Guid profileId, EmployeeType type, string userId) : base(Guid.NewGuid(), userId)
    {
        ProfileId = profileId;
        PromoteTo(type, userId);
    }

    private Employee()
    {
    }

    #endregion Constructors

    #region Properties

    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    public virtual Profile Profile { get; private set; }

    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
    public Guid ProfileId { get; private set; }

    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public EmployeeType Type { get; private set; }

    #endregion Properties

    #region Methods

    public void PromoteTo(EmployeeType type, string userId)
    {
        Type = type;
        SetUpdatedBy(userId);
    }

    #endregion Methods
}