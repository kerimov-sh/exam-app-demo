using System.Linq.Expressions;
using DemoApp.DataAccess.Entities;
using DemoApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.DataAccess.EfCore.Repositories;

public class EfExamRepository : EfRepository<Exam>, IExamRepository
{
    public EfExamRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public override async Task<ICollection<Exam>> GetAllAsync(Expression<Func<Exam, bool>>? predicate = null)
    {
	    IQueryable<Exam> queryable = DbSet
		    .Include(x => x.Student)
		    .Include(x => x.Lesson);

	    if (predicate is not null)
	    {
		    queryable = queryable.Where(predicate);
	    }

	    return await queryable.ToListAsync();
    }

    public override async Task<Exam?> GetAsync(Expression<Func<Exam, bool>> predicate)
    {
	    return await DbSet
		    .Include(x => x.Student)
		    .Include(x => x.Lesson)
		    .FirstOrDefaultAsync(predicate);
    }
}