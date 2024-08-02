using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using SharedKernel;

namespace Application.Products.Command.Create;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Product>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
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
        return Result.Success(product);
    }
}
