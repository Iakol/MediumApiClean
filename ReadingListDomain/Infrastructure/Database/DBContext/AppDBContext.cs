using Microsoft.EntityFrameworkCore;
using ReadingListDomain.Infrastructure.Database.FluentApi;
using ReadingListDomain.Infrastructure.Database.Models;

namespace ReadingListDomain.Infrastructure.Database.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<ReadingListModel>(new ReadingListModel_FluentClass());
            modelBuilder.ApplyConfiguration<StoryInReadingListModel>(new StoryInReadingListModel_FluentClass());

        }

        DbSet<ReadingListModel> ReadingLists { get; set; }

        DbSet<StoryInReadingListModel> StoryInReadingListModels { get; set; }
    }
}
