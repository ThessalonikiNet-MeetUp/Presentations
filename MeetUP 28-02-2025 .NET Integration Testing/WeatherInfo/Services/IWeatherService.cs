using WeatherInfo.Responses;

namespace WeatherInfo.Services;

public interface IWeatherService
{
      Task<WeatherInfoResponse> GetWeatherInfo(string city);    
}