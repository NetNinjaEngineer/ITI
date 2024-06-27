using ITI.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;
public class EmployeeWithProjectViewConfiguration : IEntityTypeConfiguration<EmployeeWithProjectView>
{
    public void Configure(EntityTypeBuilder<EmployeeWithProjectView> builder)
    {
        builder.HasNoKey();
        builder.Property(p => p.Project)
            .HasColumnName("ProjectName");
        builder.ToView("V_EmployeesWithProjects");
    }
}
