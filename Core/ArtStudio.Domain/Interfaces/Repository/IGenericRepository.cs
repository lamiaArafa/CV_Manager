using CV_Manager.Domain.CommonEntities;
using System.Linq.Expressions;

namespace CV_Manager.Domain.Interfaces.Repository;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    IQueryable<T> GetDbSet();
    IQueryable<T> QueryBy(Expression<Func<T, bool>> criteria);
    IQueryable<T> QueryBy(IQueryable<T> query, int skip, int take);
    IQueryable<T> QueryBy(Expression<Func<T, bool>> criteria, int skip, int take);

    IQueryable<T> Sort(IQueryable<T> source, Expression<Func<T, object>>? orderBy = null, string? orderByDirection = "asc");


    Task<T?> FindAsync(int id);
    Task<T?> FindAsync(Expression<Func<T, bool>> criteria);
    Task<bool> AnyAsync(Expression<Func<T, bool>> criteria);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Update(IEnumerable<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteRangeAsync(IEnumerable<T> entities);
    
    Task<int> GetCountAsync();
    Task<int> GetCountAsync(IQueryable<T> query);
    Task<int> GetCountAsync(Expression<Func<T, bool>> criteria);

}
