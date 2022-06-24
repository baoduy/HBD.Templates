using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MediatR.Domains.ValueObjects;

[Owned]
public class Company
{
    #region Constructors

    public Company(string name, string uen, string abn, string arbn, string can, LegalType legalType)
    {
        Name = name;
        UEN = uen;
        ABN = abn;
        ARBN = arbn;
        CAN = can;
        LegalType = LegalType;
    }

    internal Company()
    {
    }

    #endregion Constructors

    #region Properties

    [MaxLength(50)] public string ABN { get; }

    [MaxLength(50)] public string ARBN { get; }

    [MaxLength(50)] public string CAN { get; }

    public LegalType LegalType { get; }

    [MaxLength(100)] public string Name { get; }

    [MaxLength(100)] public string UEN { get; }

    #endregion Properties
}