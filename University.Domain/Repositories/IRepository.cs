using System.Linq.Expressions;

namespace University.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Remove(TEntity entity, CancellationToken cancellationToken = default);
        void RemoveRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    }
}
