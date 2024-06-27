using ITI.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;
public class CairoAlexStudentGradesViewConfiguration
    : IEntityTypeConfiguration<CairoAlexStudentGradesView>
{
    public void Configure(
        EntityTypeBuilder<CairoAlexStudentGradesView> builder)
    {
        builder.HasNoKey();
        builder.Property(p => p.StudentId)
            .HasColumnName("STDID");
        builder.Property(p => p.StudentName)
            .HasColumnName("NAME");
        builder.Property(p => p.CourseName)
            .HasColumnName("Course");
        builder.Property(p => p.SCCourseId)
            .HasColumnName("SCCID");
        builder.Property(p => p.SCStudentId)
            .HasColumnName("SCSTDID");
        builder.ToView("CairoAlexStudentGradesView");
    }
}
