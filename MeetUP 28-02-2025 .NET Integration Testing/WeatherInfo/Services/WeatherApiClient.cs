namespace WeatherInfo.Services;

public sealed class WeatherApiClient(HttpClient httpClient, IConfiguration configuration) : IDisposable
{
    public Task<HttpResponseMessage> GetWeatherInfo(string city)
    {
        var query = QueryString.Create(new Dictionary<string, string>
        {
            {"q", city},
            {"key", configuration["WeatherApiKey"]!},
            {"aqi", "no"}
        }!);
        return httpClient.GetAsync(query.ToString());
    }
    
    public void Dispose() => httpClient.Dispose();
}