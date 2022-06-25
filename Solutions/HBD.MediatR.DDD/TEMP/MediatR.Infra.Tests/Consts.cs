using System.Runtime.InteropServices;
using MediatR.Core;

namespace MediatR.Infra.Tests;

internal class Consts
{
    public static string ConnectionString =>
        RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? $"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog={SettingKeys.DbConnectionString};Integrated Security=True;Connect Timeout=300;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=True"
            : $"Data Source=192.168.1.95;Initial Catalog={SettingKeys.DbConnectionString};User Id=sa;Password=Pass@word1;";
}