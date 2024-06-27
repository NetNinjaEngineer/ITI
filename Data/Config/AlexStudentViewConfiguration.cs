using ITI.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class AlexStudentViewConfiguration : IEntityTypeConfiguration<AlexStudentView>
{
    public void Configure(EntityTypeBuilder<AlexStudentView> builder)
    {
        builder.HasNoKey();

        builder.ToView("AlexStudents");
    }
}
