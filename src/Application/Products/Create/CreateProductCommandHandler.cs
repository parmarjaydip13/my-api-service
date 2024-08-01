using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using SharedKernel;

namespace Application.Products.Create;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            request.ProductName,
            request.Price,
            request.Category
            );

        await _productRepository.AddAsync(product);
        return Result.Success<Product>(product);
    }
}
