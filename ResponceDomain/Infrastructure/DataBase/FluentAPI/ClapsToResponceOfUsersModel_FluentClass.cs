using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResponceDomain.Infrastructure.DataBase.Models;

namespace ResponceDomain.Infrastructure.DataBase.FluentAPI
{
    public class ClapsToResponceOfUsersModel_FluentClass : IEntityTypeConfiguration<ClapsToResponceOfUsersModel>
    {
        public void Configure(EntityTypeBuilder<ClapsToResponceOfUsersModel> builder)
        {
            builder.HasKey(c => new { c.UserId, c.ResponceId });

            builder.HasOne(c => c.Responce).WithMany(r => r.ClapsToResponceOfUsersModels).HasForeignKey(c => c.ResponceId);
        }
    }
}
