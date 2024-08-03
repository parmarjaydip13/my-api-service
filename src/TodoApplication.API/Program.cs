using Application;
using Carter;
using Infrastructure;
using Infrastructure.MessageBroker;
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

