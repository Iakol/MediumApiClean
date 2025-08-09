using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReadingListDomain.Infrastructure.Database.Models;

namespace ReadingListDomain.Infrastructure.Database.FluentApi
{
    public class ReadingListModel_FluentClass : IEntityTypeConfiguration<ReadingListModel>
    {
        public void Configure(EntityTypeBuilder<ReadingListModel> builder)
        {
            builder.HasKey(r => r.Id);


        }
    }
}
