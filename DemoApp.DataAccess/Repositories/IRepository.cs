using System.Linq.Expressions;
using DemoApp.DataAccess.Entities;

namespace DemoApp.DataAccess.Repositories;

public interface IRepository<T>
    where T : class, IEntity
{
    void Add(T entity);

    void Update(T entity);

    void Delete(T entity);

    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);

    Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null);
}