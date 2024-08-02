using Application.Abstractions.EventBus;
using Application.Products.Event;
using Infrastructure.BackGroundJobs;
using Infrastructure.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Configuration;
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
        
        services.AddMassTransit(configure =>
        {
            configure.SetKebabCaseEndpointNameFormatter();

            configure.AddConsumer<ProductCreatedEventConsumer>();

            configure.UsingRabbitMq((context, configurator) =>
            {
                var settings = context.GetRequiredService<MessageBrokerSettings>();

                configurator.Host(new Uri(settings.Host), h =>
                {
                    h.Username(settings.UserName);
                    h.Password(settings.Password);
                });

                configurator.ConfigureEndpoints(context);
            });
        });

        services.AddTransient<IEventBus, EventBus>();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutBoxMessagesJob));

            configure.
                AddJob<ProcessOutBoxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithIdentity("Minutes Trigger")
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
