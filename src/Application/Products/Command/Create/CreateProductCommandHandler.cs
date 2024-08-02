using Application.Abstractions.EventBus;
using Application.Abstractions.Messaging;
using Application.Products.Event;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using SharedKernel;

namespace Application.Products.Command.Create;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private IEventBus _eventBus;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _eventBus = eventBus;
    }

    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            request.ProductName,
            request.Price,
            request.Category
            );

        _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _eventBus.PublishAsync(new ProductCreatedEvent
        {
            Id = product.Id,
            ProductName = product.ProductName
        }, cancellationToken);

        return Result.Success(product);
    }
}
