using DemoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.DataAccess.EfCore.Configs;

public class LessonConfig : EntityConfig<Lesson>
{
    public override void Configure(EntityTypeBuilder<Lesson> builder)
    {
        base.Configure(builder);

        builder.ToTable("Lessons");

        builder.HasKey(x => x.Code);

        builder.Property(x => x.Code)
            .HasColumnType("nchar(3)");

        builder.Property(x => x.Name)
            .HasMaxLength(30);

        builder.Property(x => x.Class)
            .HasColumnType("numeric(2, 0)");

        builder.Property(x => x.TeacherName)
            .HasMaxLength(20);

        builder.Property(x => x.TeacherSurname)
            .HasMaxLength(20);
    }
}