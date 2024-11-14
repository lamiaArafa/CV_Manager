using CV_Manager.Domain.CommonEntities;
using CV_Manager.Domain.Interfaces.Repository;
using CV_Manager.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CV_Manager.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
    {
        private ApplicationDbContext _context;
        private readonly DbSet<T> DbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }
        public IQueryable<T> GetDbSet()
        {
            return DbSet;
        }

        public IQueryable<T> QueryBy(Expression<Func<T, bool>> criteria)
        {
            return DbSet.Where(criteria);
        }

        public IQueryable<T> QueryBy(IQueryable<T> query, int skip, int take)
        {
            return query.Skip(skip).Take(take);
        }

        public IQueryable<T> QueryBy(Expression<Func<T, bool>> criteria, int skip, int take)
        {
            return QueryBy(criteria).Skip(skip).Take(take);
        }

        public IQueryable<T> Sort(IQueryable<T> query, Expression<Func<T, object>>? orderBy = null, string? orderByDirection = "asc")
        {
            if (orderBy != null)
            {
                if (orderByDirection == "asc")
                    query = query.OrderBy(orderBy);
                else
                    query = query.OrderByDescending(orderBy);
            }
            return query;
        }

        public async Task<T?> FindAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<T?> FindAsync(Expression<Func<T, bool>> criteria)
        {
            return await DbSet.FirstOrDefaultAsync(criteria);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> criteria)
        {
            return await QueryBy(criteria).AnyAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }


        public void Update(T entity)
        {
            _context.Update(entity);
            _context.SaveChangesAsync();

        }
        public void Update(IEnumerable<T> entities)
        {
            _context.UpdateRange(entities);
            _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => { DbSet.Remove(entity); });
            await _context.SaveChangesAsync();

        }
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            await Task.Run(() => { DbSet.RemoveRange(entities); });
            await _context.SaveChangesAsync();

        }

        public async Task<int> GetCountAsync()
        {
            return await DbSet.CountAsync();
        }
        public async Task<int> GetCountAsync(IQueryable<T> query)
        {
            return await query.CountAsync();
        }
        public async Task<int> GetCountAsync(Expression<Func<T, bool>> criteria)
        {
            return await DbSet.CountAsync(criteria);
        }
    }
}

