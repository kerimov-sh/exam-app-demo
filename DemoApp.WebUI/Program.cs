using DemoApp.Business.AutoMapper;
using DemoApp.Business.Services;
using DemoApp.Business.Services.Implementations;
using DemoApp.DataAccess;
using DemoApp.DataAccess.EfCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
	.AddControllersWithViews()
	.AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ExamsDbContext>(opt => opt
        .UseLoggerFactory(LoggerFactory.Create(config => config.AddConsole()))
        .UseSqlServer(builder.Configuration.GetConnectionString("ExamsDbConnectionString")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<IExamService, ExamService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ILessonService, LessonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
