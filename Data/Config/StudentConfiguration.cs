using ITI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(e => e.StId);

        builder.ToTable("Student");

        builder.Property(e => e.StId)
            .ValueGeneratedNever()
            .HasColumnName("St_Id");
        builder.Property(e => e.DeptId).HasColumnName("Dept_Id");
        builder.Property(e => e.StAddress)
            .HasMaxLength(100)
            .HasColumnName("St_Address");
        builder.Property(e => e.StAge).HasColumnName("St_Age");
        builder.Property(e => e.StFname)
            .HasMaxLength(50)
            .HasColumnName("St_Fname");
        builder.Property(e => e.StLname)
            .HasMaxLength(10)
            .IsFixedLength()
            .HasColumnName("St_Lname");
        builder.Property(e => e.StSuper).HasColumnName("St_super");

        builder.HasOne(d => d.Dept).WithMany(p => p.Students)
            .HasForeignKey(d => d.DeptId)
            .HasConstraintName("FK_Student_Department");

        builder.HasOne(d => d.StSuperNavigation).WithMany(p => p.InverseStSuperNavigation)
            .HasForeignKey(d => d.StSuper)
            .HasConstraintName("FK_Student_Student");
    }
}
