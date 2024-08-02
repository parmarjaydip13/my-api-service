using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(AssemblyReference.Assembly)
                    .AddClasses(false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        services.AddSingleton<ConvertDomainEventsToOutBoxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutBoxMessagesInterceptor>()!;
            options.UseNpgsql(configuration.GetConnectionString("Database"))
                .AddInterceptors(interceptor);

        });

        return services;
    }
}
