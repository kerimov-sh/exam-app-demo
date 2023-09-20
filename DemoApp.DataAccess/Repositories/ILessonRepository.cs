using DemoApp.DataAccess.Entities;

namespace DemoApp.DataAccess.Repositories;

public interface ILessonRepository : IRepository<Lesson>
{
    Task<bool> IsExistsAsync(string code);
}