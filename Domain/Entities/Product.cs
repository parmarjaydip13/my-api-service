using Domain.Primitives;

namespace Domain.Entities;

public sealed class Product : Entity
{
    private Product(Guid productId, string productName, decimal price, Category category) : base(productId)
    {
        ProductName = productName;
        Price = price;
        Category = category;
    }

    public string ProductName { get; private set; } = null!;
    public decimal Price { get; private set; }
    public Category Category { get; private set; }
}
