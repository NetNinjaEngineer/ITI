using ITI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasKey(e => e.CrsId);

        builder.ToTable("Course");

        builder.Property(e => e.CrsId)
            .ValueGeneratedNever()
            .HasColumnName("Crs_Id");

        builder.Property(e => e.CrsDuration)
            .HasColumnName("Crs_Duration");
        builder.Property(e => e.CrsName)
            .HasMaxLength(50)
            .HasColumnName("Crs_Name");

        builder.Property(e => e.TopId)
            .HasColumnName("Top_Id");

        builder.HasOne(d => d.Top)
            .WithMany(p => p.Courses)
            .HasForeignKey(d => d.TopId)
            .HasConstraintName("FK_Course_Topic");
    }
}
