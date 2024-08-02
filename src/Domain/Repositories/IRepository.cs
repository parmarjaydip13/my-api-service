using Domain.Primitives;

namespace Domain.Repositories;

public interface IRepository<TEntity> where TEntity : Entity
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}