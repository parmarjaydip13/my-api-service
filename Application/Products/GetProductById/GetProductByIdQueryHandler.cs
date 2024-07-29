using Application.Abstractions.Messaging;
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

        if(product is null)
        {
            return Result.Failure<ProductResponse>(new Error("Product.NotFound",$"Product with id ${request.productId} not found."));
        }

        var response = new ProductResponse(product.Id,product.Name);
        return response;
    }
}
