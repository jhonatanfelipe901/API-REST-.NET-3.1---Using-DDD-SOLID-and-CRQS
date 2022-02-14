
using MYAPI.Data.Mapping;
using MyAPI.CrossCutting.Settings;
using MyAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MYAPI.Data.Context
{
    public class MyApiDBContext : DbContext
    {
        public MyApiDBContext(DbContextOptions<MyApiDBContext> options) : base(options)
        {
            
        }

        public DbSet<User> User { get; set; }
        public DbSet<Voting> Voting { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new VotingMap());


            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(Settings.ConnectionString);
        }
    }
}
