using Microsoft.EntityFrameworkCore;
using ResponceDomain.Infrastructure.DataBase.FluentAPI;
using ResponceDomain.Infrastructure.DataBase.Models;

namespace ResponceDomain.Infrastructure.DataBase.DBContex
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<ResponceModel>(new ResponceModel_FluentClass());
            modelBuilder.ApplyConfiguration<ClapsToResponceOfUsersModel>(new ClapsToResponceOfUsersModel_FluentClass());

        }

        public DbSet<ClapsToResponceOfUsersModel> Claps { get; set; }

        public DbSet<ResponceModel> Responces { get; set; }
    }
}
