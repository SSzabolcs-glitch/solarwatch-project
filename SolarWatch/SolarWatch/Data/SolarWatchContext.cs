using Microsoft.EntityFrameworkCore;
using SolarWatch.Models;

namespace SolarWatch.Data
{
    public class SolarWatchContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<SunsetSunrise> SunsetSunrises { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost,1433; Database=SolarWatch; User Id=sa; Password=KYSCobalt2077; Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Configure the City entity - making the 'Name' unique
            builder.Entity<City>()
                .HasIndex(u => u.Name)
                .IsUnique();

            builder.Entity<City>()
                .HasOne(c => c.SunsetSunrise)
                .WithOne(ss => ss.City)
                .HasForeignKey<SunsetSunrise>(ss => ss.CityId);
        }
    }
}
