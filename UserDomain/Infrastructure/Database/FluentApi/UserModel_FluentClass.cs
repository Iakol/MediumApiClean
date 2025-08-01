
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserDomain.Infrastructure.Database.Models;

namespace MediumDataBaseManagerAzureApi.Data.FluentApi.User_Fluent
{
    public class UserModel_FluentClass : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            // primary Key
            builder.HasKey(x => x.Id);
            // Propetries
            // Relationship



            
            //UserStories in Story Wrapper FluentClass

            //User Profile Cred
            // AboutContent in describe in AboutContent FluentClass

            //UUser to Folow


            // Other Configuration
        }
    }
}
