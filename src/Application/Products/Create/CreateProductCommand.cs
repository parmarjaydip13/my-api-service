
using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Primitives;

namespace Application.Products.Create;

public record CreateProductCommand(string ProductName, decimal Price, Category Category) : ICommand<Product>;
