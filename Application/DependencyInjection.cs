using Application.Abstractions.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly);
            cfg.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
        });
        return services;
    }
}
