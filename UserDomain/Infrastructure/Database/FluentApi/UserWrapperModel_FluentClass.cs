
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDomain.Infrastructure.Database.Models;

namespace MediumDataBaseManagerAzureApi.Data.FluentApi.User_Fluent
{
    public class UserWrapperModel_FluentClass : IEntityTypeConfiguration<UserWrapperModel>
    {
        public void Configure(EntityTypeBuilder<UserWrapperModel> builder)
        {
            // primary Key
            builder.HasKey(x => x.UserId);
            // Propetries
            // Relationship


            builder.HasOne(u => u.User).WithOne(u => u.UserWrapper).HasForeignKey<UserWrapperModel>(u => u.UserId);



            //User Profile Cred
            builder.HasOne(u => u.UserMemberShip).WithOne(m => m.UserWrapper).HasForeignKey<UserMemberShipModel>(m => m.UserWrapperId);
            builder.HasOne(u => u.Profile).WithOne(m => m.User).HasForeignKey<UserProfileModel>(m => m.UserWrapperId);



            // Other Configuration
        }
    }
}
