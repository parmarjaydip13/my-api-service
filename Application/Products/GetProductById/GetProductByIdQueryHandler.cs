using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Repositories;
using SharedKernel;

namespace Application.Products.GetProductById;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
{

    private readonly IProductRepository _repository;

    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.productId);

        return Result.Failure<ProductResponse>(ProductError.NotFound(request.productId));

        if (product is null)
        {
            return Result.Failure<ProductResponse>(ProductError.NotFound(request.productId));
        }

        var response = new ProductResponse(product.ProductID, product.ProductName );
        return response;
    }
}
