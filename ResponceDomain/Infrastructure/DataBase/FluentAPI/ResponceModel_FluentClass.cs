using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ResponceDomain.Infrastructure.DataBase.Models;

namespace ResponceDomain.Infrastructure.DataBase.FluentAPI
{
    public class ResponceModel_FluentClass : IEntityTypeConfiguration<ResponceModel>
    {
        public void Configure(EntityTypeBuilder<ResponceModel> builder)
        {
            builder.HasKey(r => r.ResponceId);
        }
    }
}
