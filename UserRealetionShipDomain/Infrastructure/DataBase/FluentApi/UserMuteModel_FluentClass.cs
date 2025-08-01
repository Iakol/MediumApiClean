using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Infrastructure.DataBase.FluentApi
{
    public class UserMuteModel_FluentClass : IEntityTypeConfiguration<UserMuteModel>
    {
        public void Configure(EntityTypeBuilder<UserMuteModel> builder)
        {
            builder.HasKey(s => new { s.UserId, s.MuteId });

        }
    }
}
