using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherInfo.Entities;

namespace WeatherInfo.Persistence.Configurations;

public class WeatherSearchEntryConfiguration : IEntityTypeConfiguration<WeatherSearchEntry>
{
    public void Configure(EntityTypeBuilder<WeatherSearchEntry> builder)
    {
        builder.ToTable("WeatherSearchEntries");
        builder.Property(e => e.Id)
            .UseIdentityAlwaysColumn();
        
        builder.Property(e => e.LocationName)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(e => e.OccuredAt)
            .IsRequired();
        
        builder.Property(e => e.IsSuccess)
            .IsRequired();
    }
}