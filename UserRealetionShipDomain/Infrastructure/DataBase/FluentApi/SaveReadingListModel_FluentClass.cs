using Microsoft.EntityFrameworkCore;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Infrastructure.DataBase.FluentApi
{
    public class SaveReadingListModel_FluentClass : IEntityTypeConfiguration<SaveReadingListModel>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<SaveReadingListModel> builder)
        {
            builder.HasKey(s => new { s.UserId, s.SaveReadingListId });
        }
    }
}
