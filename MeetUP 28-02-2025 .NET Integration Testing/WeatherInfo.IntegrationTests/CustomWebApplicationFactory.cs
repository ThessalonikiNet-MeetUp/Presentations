using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Testcontainers.PostgreSql;
using WeatherInfo.Persistence;
using WeatherInfo.Services;

namespace WeatherInfo.IntegrationTests;

public class CustomWebApplicationFactory: WebApplicationFactory<Program>, IAsyncLifetime
{
    public const string ExistingCity = "Thessaloniki";
    public const string NotExistingCity = "NotExistingCity";
    
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("integrationTests")
        .WithPortBinding(5433, 5432)
        .Build();
    private readonly WeatherApiServer _weatherApiServer = new ();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        
        builder.ConfigureTestServices(services =>
        {
            var dbContextDescriptor = services.Single(
                d => d.ServiceType == 
                     typeof(IDbContextOptionsConfiguration<WeatherDbContext>));
            services.Remove(dbContextDescriptor);
            services.AddDbContext<WeatherDbContext>(options => options.UseNpgsql(_dbContainer.GetConnectionString()));
            
            var weatherApiClientDescriptor = services.Single(d => d.ServiceType == typeof(WeatherApiClient));
            services.Remove(weatherApiClientDescriptor);
            services.AddHttpClient<WeatherApiClient>(
                client =>
                {
                    client.BaseAddress = new Uri(_weatherApiServer.Url);
                });
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
        
        _weatherApiServer.Start();
        _weatherApiServer.SetupExistingCity(ExistingCity);
        _weatherApiServer.SetupNotExistingCity(NotExistingCity);
    }

    public new async Task DisposeAsync()
    {
        _weatherApiServer.Dispose();
        await _dbContainer.DisposeAsync();
    }
}