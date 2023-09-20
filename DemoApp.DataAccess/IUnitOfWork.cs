using DemoApp.DataAccess.Repositories;

namespace DemoApp.DataAccess;

public interface IUnitOfWork : IDisposable
{
    ILessonRepository Lessons { get; }

    IStudentRepository Students { get; }

    IExamRepository Exams { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    void DiscardChanges();
}