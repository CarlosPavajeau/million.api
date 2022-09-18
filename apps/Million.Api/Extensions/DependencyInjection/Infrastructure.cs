using Microsoft.EntityFrameworkCore;
using Million.Shared.Infrastructure.Persistence;

namespace Million.Api.Extensions.DependencyInjection;

public static class Infrastructure
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<MillionDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .UseSnakeCaseNamingConvention()
                .EnableDetailedErrors();
        }, ServiceLifetime.Transient);
        
        services.AddTransient<DbContext, MillionDbContext>();

        return services;
    }
}
