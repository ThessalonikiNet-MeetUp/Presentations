using Microsoft.EntityFrameworkCore;
using WeatherInfo.Entities;

namespace WeatherInfo.Persistence;

public class WeatherDbContext(DbContextOptions<WeatherDbContext> options) : DbContext(options)
{
    public DbSet<WeatherSearchEntry> WeatherSearchEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WeatherDbContext).Assembly);
    }
}