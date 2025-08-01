using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Infrastructure.DataBase.FluentApi
{
    public class UserBlockModel_FluentClass : IEntityTypeConfiguration<UserBlockModel>
    {
        public void Configure(EntityTypeBuilder<UserBlockModel> builder)
        {
            builder.HasKey(s => new { s.UserId, s.BlockId });

        }
    }
}
