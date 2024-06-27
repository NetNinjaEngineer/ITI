using ITI.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;
public class StudentsPerDepartmentConfiguration : IEntityTypeConfiguration<StudentsPerDepartment>
{
    public void Configure(EntityTypeBuilder<StudentsPerDepartment> builder)
    {
        builder.HasNoKey();
    }
}
