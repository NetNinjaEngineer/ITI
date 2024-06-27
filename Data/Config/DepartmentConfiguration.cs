using ITI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasKey(e => e.DeptId);

        builder.ToTable("Department");

        builder.Property(e => e.DeptId)
            .ValueGeneratedNever()
            .HasColumnName("Dept_Id");
        builder.Property(e => e.DeptDesc)
            .HasMaxLength(100)
            .HasColumnName("Dept_Desc");
        builder.Property(e => e.DeptLocation)
            .HasMaxLength(50)
            .HasColumnName("Dept_Location");
        builder.Property(e => e.DeptManager).HasColumnName("Dept_Manager");
        builder.Property(e => e.DeptName)
            .HasMaxLength(50)
            .HasColumnName("Dept_Name");
        builder.Property(e => e.ManagerHiredate).HasColumnName("Manager_hiredate");

        builder.HasOne(d => d.DeptManagerNavigation).WithMany(p => p.Departments)
            .HasForeignKey(d => d.DeptManager)
            .HasConstraintName("FK_Department_Instructor");
    }
}
