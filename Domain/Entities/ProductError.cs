

using SharedKernel;

namespace Domain.Entities;

public static class ProductError
{
    public static Error InvalidName => Error.Validation("Product.InvalidName", "Product name is not valid.");
    public static Error NotFound(Guid guid) => Error.NotFound("Product.NotFound", $"Product with Id '{guid}' not found.");
}
