using ITI.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;
public class StudentWithAddressConfiguration : IEntityTypeConfiguration<StudentWithAddress>
{
    public void Configure(EntityTypeBuilder<StudentWithAddress> builder)
    {
        builder.HasNoKey();
        builder.Property(p => p.StudentId)
            .HasColumnName("St_Id");
        builder.Property(p => p.FirstName)
            .HasColumnName("St_Fname");
        builder.Property(p => p.Address)
            .HasColumnName("St_Address");
    }
}
