using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Products.Event;
public class ProductCreatedEventConsumer : IConsumer<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventConsumer> _logger;

    public ProductCreatedEventConsumer(ILogger<ProductCreatedEventConsumer> logger)
    {
        this._logger = logger;
    }

    public Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        _logger.LogInformation("Product Created : {@product}", context.Message);
        return Task.CompletedTask;
    }
}
