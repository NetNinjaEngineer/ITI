using ITI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;

public class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.HasKey(e => e.TopId);

        builder.ToTable("Topic");

        builder.Property(e => e.TopId)
            .ValueGeneratedNever()
            .HasColumnName("Top_Id");
        builder.Property(e => e.TopName)
            .HasMaxLength(50)
            .HasColumnName("Top_Name");
    }
}
