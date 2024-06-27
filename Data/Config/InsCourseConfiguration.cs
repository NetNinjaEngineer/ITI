using ITI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class InsCourseConfiguration : IEntityTypeConfiguration<InsCourse>
{
    public void Configure(EntityTypeBuilder<InsCourse> builder)
    {
        builder.HasKey(e => new { e.InsId, e.CrsId });

        builder.ToTable("Ins_Course");

        builder.Property(e => e.InsId).HasColumnName("Ins_Id");
        builder.Property(e => e.CrsId).HasColumnName("Crs_Id");
        builder.Property(e => e.Evaluation).HasMaxLength(50);

        builder.HasOne(d => d.Crs).WithMany(p => p.InsCourses)
            .HasForeignKey(d => d.CrsId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Ins_Course_Course");

        builder.HasOne(d => d.Ins).WithMany(p => p.InsCourses)
            .HasForeignKey(d => d.InsId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Ins_Course_Instructor");
    }
}
