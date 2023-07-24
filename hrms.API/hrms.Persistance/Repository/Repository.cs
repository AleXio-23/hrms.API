using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace hrms.Persistance.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {

        protected readonly HrmsAppDbContext _context;

        public Repository(HrmsAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity?> Get(int id, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().FindAsync(new object?[] { id, cancellationToken }, cancellationToken: cancellationToken).ConfigureAwait(false);
        }


        public async Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken)
        {
            var newData = await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return newData.Entity;
        }

        public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            var entity = await Get(id, cancellationToken).ConfigureAwait(false) ?? throw new ArgumentException($"Record with id: {id} not found.");
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        public async Task Delete(TEntity entity, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        public async Task Delete(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            var getTEntity = await SingleOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false) ?? throw new ArgumentException($"Record with {predicate.Name} not found");
            _context.Set<TEntity>().Remove(getTEntity);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(predicate, cancellationToken).ConfigureAwait(false);
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }

        public async Task<TEntity?> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task DeleteRange(TEntity[] entities, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        public async Task DeleteRange(List<TEntity> entities, CancellationToken cancellationToken)
        {
            _context.Set<TEntity>().RemoveRange(entities);
            await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}

