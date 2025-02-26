using System.Text.Json;
using WeatherInfo.Entities;
using WeatherInfo.Persistence;
using WeatherInfo.Responses;

namespace WeatherInfo.Services;

public class WeatherService(WeatherApiClient client, WeatherDbContext context) : IWeatherService
{
    public async Task<WeatherInfoResponse> GetWeatherInfo(string city)
    {
        ArgumentException.ThrowIfNullOrEmpty(city, nameof(city));
        
        var response = await client.GetWeatherInfo(city);
        var searchEntry = new WeatherSearchEntry
        {
            LocationName = city,
            IsSuccess = response.IsSuccessStatusCode,
            OccuredAt = DateTime.UtcNow
        };
        await context.WeatherSearchEntries.AddAsync(searchEntry);
        await context.SaveChangesAsync();
        
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<WeatherInfoResponse>(await response.Content.ReadAsStringAsync(), serializerOptions)!;
        }
        
        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync(), serializerOptions)!;
        throw new Exception(errorResponse.Error.Message){ Data = { { "Code", errorResponse.Error.Code } } };
    }
}