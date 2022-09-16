using Microsoft.Extensions.DependencyInjection;
using Million.Shared.Infrastructure.Persistence;

namespace Million.Api.Test.Shared;

public class ApplicationContextTestCase : IClassFixture<ApplicationTestCase>
{
    private readonly ApplicationTestCase _applicationTestCase;
    protected readonly HttpClient Client;

    public ApplicationContextTestCase(ApplicationTestCase applicationTestCase)
    {
        _applicationTestCase = applicationTestCase;
        Client = CreateHttpClient();
    }

    private HttpClient CreateHttpClient()
    {
        return _applicationTestCase.CreateDefaultClient();
    }

    protected MillionDbContext GetDbContext()
    {
        var scope = _applicationTestCase.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MillionDbContext>();

        return dbContext;
    }
}
