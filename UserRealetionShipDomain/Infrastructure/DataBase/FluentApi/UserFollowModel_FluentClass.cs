using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Infrastructure.DataBase.FluentApi
{
    public class UserFollowModel_FluentClass : IEntityTypeConfiguration<UserFollowModel>
    {
        public void Configure(EntityTypeBuilder<UserFollowModel> builder)
        {
            builder.HasKey(s => new { s.UserId, s.FollowId });

        }
    }
}
