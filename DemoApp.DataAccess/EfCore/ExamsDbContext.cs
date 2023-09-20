using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.DataAccess.EfCore;

public class ExamsDbContext : DbContext
{
    public ExamsDbContext(DbContextOptions<ExamsDbContext> options)
        : base(options)
    {
    }

    //Uncomment for create migration
    //public ExamsDbContext()
    //{
    //}

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	// optionsBuilder.UseSqlServer("Server=.;Database=ExamsDb;Trusted_Connection=true;");
	//}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}