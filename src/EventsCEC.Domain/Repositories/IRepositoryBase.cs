using System.Linq.Expressions;

namespace EventsCEC.Domain.Repositories;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    void DeleteAsync(TEntity entity);
    Task<IQueryable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IQueryable<TEntity>> GetAllBy(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IQueryable<TEntity>> GetAllByOrderBy<TKey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TKey>> order, bool asc = true, params Expression<Func<TEntity, object>>[] includeProperties);
    Task<IEnumerable<TEntity>> GetAsync(int limit);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity?> Exists(Expression<Func<TEntity, bool>> predicate);
    int SaveChanges();
    Task<int> SaveChangesAsync();
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object>>[] includes);
}
