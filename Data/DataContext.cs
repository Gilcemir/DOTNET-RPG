using DOTNET_RPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DOTNET_RPG.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<Fighter> Fighters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chant> Chants { get; set; }
        public DbSet<Skill> Skills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill {Id = 1, Name = "Kataguruma", Damage = 3},
                new Skill {Id = 2, Name = "Punch", Damage = 5},
                new Skill {Id = 3, Name = "Katagatame", Damage = 98}
            );
        }
    }
}