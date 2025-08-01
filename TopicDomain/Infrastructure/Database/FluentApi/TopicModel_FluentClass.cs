using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TopicDomain.Infrastructure.Database.Models;

namespace TopicDomain.Infrastructure.Database.FluentApi
{
    public class TopicModel_FluentClass : IEntityTypeConfiguration<TopicModel>
    {
        public void Configure(EntityTypeBuilder<TopicModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(t => t.Parent).WithMany(p => p.SubTopic).HasForeignKey(t => t.ParentId);

        }
    }
}
