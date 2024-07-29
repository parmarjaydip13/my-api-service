using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using SharedKernel;

namespace Application.Products.Create;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _ProductRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _ProductRepository = productRepository;
    }

    public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var productName = request.Name;
        var product = new Product { Name = productName };
        await _ProductRepository.AddAsync(product);
        return Result.Success<Product>(product);
    }
}
