using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

internal sealed class ProductRepository : Repository<Product>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
