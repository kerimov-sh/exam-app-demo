using DemoApp.DataAccess.Entities;

namespace DemoApp.DataAccess.Repositories;

public interface IStudentRepository : IRepository<Student>
{
    Task<bool> IsExistsAsync(int number);
}