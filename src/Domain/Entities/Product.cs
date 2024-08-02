using Domain.DomainEvents;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public sealed class Product : AggregateRoot
{
    private Product(Guid id, string productName, decimal price, Category category) : base(id)
    {
        ProductName = productName;
        Price = price;
        Category = category;
    }

    public string ProductName { get; private set; }
    public decimal Price { get; private set; }
    public Category Category { get; private set; }

    public static Product Create(string productName, decimal price, Category category)
    {
        var product = new Product(Guid.NewGuid(), productName, price, category);
        product.RaiseDomainEvent(new ProductCreateDomainEvent(product.Id));
        return product;
    }
}
