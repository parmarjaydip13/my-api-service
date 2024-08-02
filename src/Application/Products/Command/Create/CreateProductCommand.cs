using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Enums;

namespace Application.Products.Command.Create;

public record CreateProductCommand(string ProductName, decimal Price, Category Category) : ICommand<Product>;
