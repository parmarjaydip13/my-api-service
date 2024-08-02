using Application.Abstractions.Messaging;
using Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Products.Event;
internal sealed class ProductCreateDomainEventHandler : IDomainEventHandler<ProductCreateDomainEvent>
{
    private readonly ILogger<ProductCreateDomainEventHandler> _logger;

    public ProductCreateDomainEventHandler(ILogger<ProductCreateDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductCreateDomainEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Domain Event Called {id}", notification.ProductId);
        return Task.CompletedTask;
    }
}
