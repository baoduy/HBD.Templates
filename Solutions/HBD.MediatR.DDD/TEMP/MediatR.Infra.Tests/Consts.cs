using System.Runtime.InteropServices;

namespace MediatR.Infra.Tests;

internal class Consts
{
    #region Fields

    private const string DbName = "TEMPDb";

    #endregion Fields

    #region Properties

    public static string ConnectionString =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={DbName};Integrated Security=True;Connect Timeout=300;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=True"
            : $"Data Source=localhost;Initial Catalog={DbName};User Id=sa;Password=Pass@word1;";

    #endregion Properties
}