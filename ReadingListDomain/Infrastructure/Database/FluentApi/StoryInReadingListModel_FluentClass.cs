using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingListDomain.Infrastructure.Database.Models;

namespace ReadingListDomain.Infrastructure.Database.FluentApi
{
    public class StoryInReadingListModel_FluentClass : IEntityTypeConfiguration<StoryInReadingListModel>
    {
        public void Configure(EntityTypeBuilder<StoryInReadingListModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.ReadingList).WithMany(r => r.SaveStories).HasForeignKey(x => x.ReadingListId);
        }
    }
}
