using Application.Abstractions;
using Infrastructure.BackGroundJobs;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;

namespace Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services
            .Scan(
                selector => selector
                    .FromAssemblies(AssemblyReference.Assembly)
                    .AddClasses(false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutBoxMessagesJob));

            configure.
                AddJob<ProcessOutBoxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInMinutes(30)
                                        .RepeatForever()));

        });

        services.AddQuartzServer(options =>
        {
            options.WaitForJobsToComplete = true;
        });

        return services;
    }
}
