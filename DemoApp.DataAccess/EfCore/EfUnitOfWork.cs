using DemoApp.DataAccess.EfCore.Repositories;
using DemoApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.DataAccess.EfCore;

public class EfUnitOfWork : IUnitOfWork
{
    private readonly ExamsDbContext _dbContext;
    private readonly object _locker = new();
    private readonly IDictionary<string, object> _repositoryCache = new Dictionary<string, object>();

    public EfUnitOfWork(ExamsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public ILessonRepository Lessons =>
        GetRepository<EfLessonRepository>();

    public IStudentRepository Students =>
        GetRepository<EfStudentRepository>();

    public IExamRepository Exams =>
        GetRepository<EfExamRepository>();

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void DiscardChanges()
    {
        _dbContext.ChangeTracker.Clear();
    }

    private T GetRepository<T>()
    {
        lock (_locker)
        {
            var repoType = typeof(T);
            var repoTypeName = repoType.FullName!;

            if (_repositoryCache.TryGetValue(repoTypeName, out var repo))
            {
                return (T)repo;
            }

            var instance = Activator.CreateInstance(repoType, _dbContext)!;
            _repositoryCache.Add(repoTypeName, instance);

            return (T)instance;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _dbContext.Dispose();
    }
}