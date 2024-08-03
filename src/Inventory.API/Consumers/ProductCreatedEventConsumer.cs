using MassTransit;
using MessageContract;

namespace Inventory.API.Consumers;

public class ProductCreatedEventConsumer2 : IConsumer<ProductCreatedEvent>
{
    private readonly ILogger<ProductCreatedEventConsumer2> _logger;

    public ProductCreatedEventConsumer2(ILogger<ProductCreatedEventConsumer2> logger)
    {
        this._logger = logger;
    }

    public Task Consume(ConsumeContext<ProductCreatedEvent> context)
    {
        _logger.LogInformation("Product Created consumer 2: {@product}", context.Message);
        return Task.CompletedTask;
    }
}
