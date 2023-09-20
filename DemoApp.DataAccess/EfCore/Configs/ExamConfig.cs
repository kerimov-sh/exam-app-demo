using DemoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.DataAccess.EfCore.Configs;

public class ExamConfig : EntityConfig<Exam>
{
    public override void Configure(EntityTypeBuilder<Exam> builder)
    {
        base.Configure(builder);

        builder.ToTable("Exams");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.LessonCode)
            .HasColumnType("nchar(3)");

        builder.HasOne(x => x.Lesson)
            .WithMany()
            .HasForeignKey(x => x.LessonCode)
            .HasPrincipalKey(x => x.Code);

        builder.Property(x => x.StudentNumber)
            .HasColumnType("numeric(5, 0)");

        builder.HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.StudentNumber)
            .HasPrincipalKey(x => x.Number);

        builder.Property(x => x.ExamDate)
            .HasColumnType("date");

        builder.Property(x => x.Score)
            .HasColumnType("numeric(1, 0)");
    }
}