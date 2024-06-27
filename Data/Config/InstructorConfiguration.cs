using ITI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder.HasKey(keyExpression: e => e.InsId);

        builder.ToTable("Instructor");

        builder.Property(e => e.InsId)
            .ValueGeneratedNever()
            .HasColumnName("Ins_Id");
        builder.Property(e => e.DeptId).HasColumnName("Dept_Id");
        builder.Property(e => e.InsDegree)
            .HasMaxLength(50)
            .HasColumnName("Ins_Degree");
        builder.Property(e => e.InsName)
            .HasMaxLength(50)
            .HasColumnName("Ins_Name");
        builder.Property(e => e.Salary).HasColumnType("money");

        builder.HasOne(d => d.Dept).WithMany(p => p.Instructors)
            .HasForeignKey(d => d.DeptId)
            .HasConstraintName("FK_Instructor_Department");
    }
}
