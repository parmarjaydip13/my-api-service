
using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Products.Create;

public record CreateProductCommand(string Name) : ICommand<Product>;
