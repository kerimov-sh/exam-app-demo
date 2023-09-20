using System.Linq.Expressions;
using DemoApp.DataAccess.Entities;
using DemoApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.DataAccess.EfCore.Repositories;

public abstract class EfRepository<T> : IRepository<T>
    where T : class, IEntity
{
    protected readonly DbSet<T> DbSet;

    protected EfRepository(DbContext dbContext)
    {
        DbSet = dbContext.Set<T>();
    }

    public virtual void Add(T entity)
    {
        DbSet.Add(entity);
    }

    public virtual void Update(T entity)
    {
        DbSet.Update(entity);
    }

    public virtual void Delete(T entity)
    {
        DbSet.Remove(entity);
    }

    public virtual async Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await DbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null)
    {
        return predicate is null
            ? await DbSet.ToListAsync()
            : await DbSet.Where(predicate).ToListAsync();
    }
}