using Microsoft.EntityFrameworkCore;
using WeatherInfo.Persistence;
using WeatherInfo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<WeatherDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("WeatherContext")));
builder.Services.AddHttpClient<WeatherApiClient>(
    client =>
    {
        client.BaseAddress = new Uri("http://api.weatherapi.com/v1/current.json");
    });
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

/// <summary>
/// This is needed for integration tests to be able to use the Program class as a type parameter for WebApplicationFactory.
/// </summary>
public partial class Program { }