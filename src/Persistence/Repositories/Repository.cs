using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly ApplicationDbContext DbContext;

    protected Repository(ApplicationDbContext applicationDbContext)
    {
        DbContext = applicationDbContext;
    }

    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Remove(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = DbContext.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        return entity;
    }
}
