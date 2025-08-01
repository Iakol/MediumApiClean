using Microsoft.EntityFrameworkCore;
using UserRealetionShipDomain.Infrastructure.DataBase.FluentApi;
using UserRealetionShipDomain.Infrastructure.DataBase.Model;

namespace UserRealetionShipDomain.Infrastructure.DataBase.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new UserMuteModel_FluentClass());
            modelBuilder.ApplyConfiguration(new UserFollowModel_FluentClass());
            modelBuilder.ApplyConfiguration(new UserBlockModel_FluentClass());
            modelBuilder.ApplyConfiguration(new SaveReadingListModel_FluentClass());

            


        }

        DbSet<SaveReadingListModel> SaveReadingList { get; set; }
        DbSet<UserBlockModel> UserBlocks { get; set; }
        DbSet<UserFollowModel> UserFollows { get; set; }
        DbSet<UserMuteModel> UserMute { get; set; }

    }
}
