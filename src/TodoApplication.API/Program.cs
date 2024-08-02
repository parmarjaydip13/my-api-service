using Application;
using Application.Abstractions.EventBus;
using Application.Products.Event;
using Carter;
using Infrastructure;
using Infrastructure.MessageBroker;
using MassTransit;
using Microsoft.Extensions.Options;
using Persistence;
using Serilog;
using TodoApplication.API.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MessageBrokerSettings>(builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);


builder.Services
    .AddInfrastructure()
    .AddApplication()
    .AddPersistence(builder.Configuration);



//builder.Services.AddMassTransit(configure =>
//{
//    configure.SetKebabCaseEndpointNameFormatter();
    
//    configure.AddConsumer<ProductCreatedEventConsumer>();

//    configure.UsingRabbitMq((context, configurator) =>
//    {
//        var settings = context.GetRequiredService<MessageBrokerSettings>();

//        configurator.Host(new Uri(settings.Host), h =>
//        {
//            h.Username(settings.UserName);
//            h.Password(settings.Password);
//        });

//        configurator.ConfigureEndpoints(context);
//    });
//});

//builder.Services.AddTransient<IEventBus, EventBus>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddCarter();

builder.Host.UseSerilog((context, loggerConfig) =>
{
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLogContextMiddleware>();

app.UseExceptionHandler();

app.UseSerilogRequestLogging();

app.MapCarter();

app.Run();

