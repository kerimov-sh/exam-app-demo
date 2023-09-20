using DemoApp.DataAccess.Entities;
using DemoApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.DataAccess.EfCore.Repositories;

public class EfStudentRepository : EfRepository<Student>, IStudentRepository
{
    public EfStudentRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> IsExistsAsync(int number)
    {
        return await DbSet.CountAsync(s => s.Number == number) > 0;
    }
}