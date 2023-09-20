using DemoApp.DataAccess.Entities;
using DemoApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.DataAccess.EfCore.Repositories;

public class EfLessonRepository : EfRepository<Lesson>, ILessonRepository
{
    public EfLessonRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsExistsAsync(string code)
    {
        return await DbSet.CountAsync(x => x.Code == code) > 0;
    }
}