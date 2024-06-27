using ITI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class StudCourseConfiguration : IEntityTypeConfiguration<StudCourse>
{
    public void Configure(EntityTypeBuilder<StudCourse> builder)
    {
        builder.HasKey(e => new { e.CrsId, e.StId });

        builder.ToTable("Stud_Course");

        builder.Property(e => e.CrsId).HasColumnName("Crs_Id");
        builder.Property(e => e.StId).HasColumnName("St_Id");

        builder.HasOne(d => d.Crs).WithMany(p => p.StudCourses)
            .HasForeignKey(d => d.CrsId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Stud_Course_Course");

        builder.HasOne(d => d.St).WithMany(p => p.StudCourses)
            .HasForeignKey(d => d.StId)
            .HasConstraintName("FK_Stud_Course_Student");
    }
}
