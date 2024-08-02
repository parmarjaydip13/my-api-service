using Domain.Repositories;

namespace Persistence;
internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext) => _dbContext = dbContext;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
         await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
