using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Testcontainers.PostgreSql;
using WeatherInfo.Persistence;

namespace WeatherInfo.IntegrationTests;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("integrationTests")
        .WithPortBinding(5433, 5432)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        
        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == 
                     typeof(IDbContextOptionsConfiguration<WeatherDbContext>));
            services.Remove(dbContextDescriptor!);
            
            services.AddDbContext<WeatherDbContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        var optionsBuilder = new DbContextOptionsBuilder<WeatherDbContext>()
            .UseNpgsql(_dbContainer.GetConnectionString());
        var dbContext = new WeatherDbContext(optionsBuilder.Options);
        await dbContext.Database.MigrateAsync();
        // Seeding the database with data can happen here
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}