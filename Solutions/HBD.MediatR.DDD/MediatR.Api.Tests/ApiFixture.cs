using MediatR.Api.Tests.TestClasses;
using Microsoft.Extensions.DependencyInjection;

namespace MediatR.Api.Tests;

public class ApiFixture : IDisposable, IAsyncDisposable
{
    private readonly TestApi _api;

    public ApiFixture()
    {
        _api = new TestApi();
        _api.CreateDefaultClient();
    }

    public IServiceScope ScopeServices => _api.ScopeServices;

    public HttpClient CreateClient() => _api.CreateClient();

    public void Dispose() => _api.Dispose();

    public ValueTask DisposeAsync() => _api.DisposeAsync();
}