using Application.Products.Create;
using Application.Products.GetProductById;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;
namespace Presentation.Products;

public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{id}", GetProduct);
        app.MapPost("/product", AddProduct).WithName("product-add");
    }

    public async Task<IResult> GetProduct(ISender sender, Guid id, CancellationToken cancellationToken)
    {
        var getProductQuery = new GetProductByIdQuery(id);
        var result = await sender.Send(getProductQuery, cancellationToken);
        return result.IsSuccess ? Results.Ok(result) : result.ToProblemResult();
    }

    public async Task<IResult> AddProduct(AddProductDTO data, ISender sender, CancellationToken cancellationToken)
    {
        var createCommand = new CreateProductCommand(data.Name, data.Email);
        var result = await sender.Send(createCommand, cancellationToken);
        return result.IsSuccess ? Results.Ok(result) : result.ToProblemResult();
    }

    public class AddProductDTO
    {
        public string Name { get; set; } = null!;

        public string? Email { get; set; }
    }
}
