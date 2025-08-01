
using MediumDataBaseManagerAzureApi.Data.FluentApi.User_Fluent;
using Microsoft.EntityFrameworkCore;
using UserDomain.Infrastructure.Database.Models;

namespace UserDomain.Infrastructure.Database.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {

        }


        //User
        public DbSet<UserWrapperModel> UserWrappers { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserMemberShipModel> UserMemberShips { get; set; }
        public DbSet<UserProfileModel> UserProfiles { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //User Wraper
            modelBuilder.ApplyConfiguration(new UserWrapperModel_FluentClass());
            modelBuilder.ApplyConfiguration(new UserModel_FluentClass());
            modelBuilder.ApplyConfiguration(new UserMemberShipModel_FluentClass());
            modelBuilder.ApplyConfiguration(new UserProfileModel_FluentClass());

        }



    }
}
