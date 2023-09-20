using DemoApp.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DemoApp.DataAccess.EfCore.Configs;

public class StudentConfig : EntityConfig<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        base.Configure(builder);

        builder.ToTable("Students");

        builder.HasKey(x => x.Number);

        builder.Property(x => x.Number)
            .HasColumnType("numeric(5, 0)")
            .ValueGeneratedNever();

        builder.Property(x => x.Name)
            .HasMaxLength(30);

        builder.Property(x => x.Surname)
            .HasMaxLength(30);

        builder.Property(x => x.Class)
            .HasColumnType("numeric(2, 0)");
    }
}