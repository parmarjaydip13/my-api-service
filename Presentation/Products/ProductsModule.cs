using Application.Products.Create;
using Application.Products.GetProductById;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SharedKernel;
using System.Threading;

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
        return result.IsSuccess ? Results.Ok(result) : Results.NotFound(result.Error);
    }

    public async Task<IResult> AddProduct(string Name, ISender sender, CancellationToken cancellationToken)
    {
        var createCommand = new CreateProductCommand(Name);
        var result = await sender.Send(createCommand, cancellationToken);
        return Results.Ok(result);
    }
}
