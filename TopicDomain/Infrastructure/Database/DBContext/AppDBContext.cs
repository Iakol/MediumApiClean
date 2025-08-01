using Microsoft.EntityFrameworkCore;
using TopicDomain.Domain;
using TopicDomain.Infrastructure.Database.FluentApi;
using TopicDomain.Infrastructure.Database.Models;

namespace TopicDomain.Infrastructure.Database.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new TopicModel_FluentClass());

        }

        public DbSet<TopicModel> Topics { get; set; }
    }
}
