using Domain.Primitives;

namespace Domain.Entities;

public sealed class Product : Entity
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
        // Here you can include additional validation or logic if needed
        return new Product(Guid.NewGuid(), productName, price, category);
    }
}
