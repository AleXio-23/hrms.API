using System.Linq.Expressions;

namespace hrms.Persistance.Repository
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken);
        Task<TEntity?> Get(int id, CancellationToken cancellationToken);
        Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
        Task Delete(int id, CancellationToken cancellationToken);
        Task Delete(TEntity entity, CancellationToken cancellationToken);
        Task Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task DeleteRange(TEntity[] entities, CancellationToken cancellationToken);
        Task DeleteRange(List<TEntity>  entities, CancellationToken cancellationToken);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
        Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken);
        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAllAsQueryable();
        IQueryable<TEntity> GetIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
