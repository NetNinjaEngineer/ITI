using ITI.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITI.ReverseEngineering.Data.Config;
public class ManagerWithTopicViewConfiguration : IEntityTypeConfiguration<ManagerWithTopicView>
{
    public void Configure(EntityTypeBuilder<ManagerWithTopicView> builder)
    {
        builder.HasNoKey();
        builder.ToView("ManagersWithTopicsTheyTeachView");
    }
}
