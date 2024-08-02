using Application.Products.Command.Create;
using Application.Products.Query.GetProductById;
using Carter;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Presentation.Extensions;
namespace Presentation.Products;

public class ProductsModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{id}", GetProduct);
        app.MapPost("/product", AddProduct).Accepts<AddProductDto>("application/json").WithName("product-add");
    }

    public async Task<IResult> GetProduct(ISender sender, Guid id, CancellationToken cancellationToken)
    {
        var getProductQuery = new GetProductByIdQuery(id);
        var result = await sender.Send(getProductQuery, cancellationToken);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemResult();
    }

    public async Task<IResult> AddProduct([FromBody] AddProductDto data, ISender sender, CancellationToken cancellationToken)
    {
        var createCommand = new CreateProductCommand(data.ProductName, data.Price, data.Category);
        var result = await sender.Send(createCommand, cancellationToken);
        return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemResult();
    }

    public record AddProductDto(string ProductName, decimal Price, Category Category);
}
