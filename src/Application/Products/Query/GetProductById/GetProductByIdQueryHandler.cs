using Application.Abstractions.Messaging;
using Domain.Errors;
using Domain.Repositories;
using SharedKernel;

namespace Application.Products.Query.GetProductById;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductResponse>
{

    private readonly IProductRepository _repository;

    public GetProductByIdQueryHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.productId, cancellationToken);

        if (product is null)
        {
            return Result.Failure<ProductResponse>(DomainErrors.ProductError.NotFound(request.productId));
        }

        var response = new ProductResponse(product.Id, product.ProductName);
        return response;
    }
}
