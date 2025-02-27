using System.Net;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using WeatherInfo.Responses;

namespace WeatherInfo.IntegrationTests.IntegrationTests;

public class WeatherInfoTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client = factory.CreateClient();
    
    [Fact]
    public async Task GetWeatherInfo_EmptyCity_Failure()
    {
        // Act
        var response = await _client.GetAsync("weatherinfo?city=");

        // Assert
        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errorResponse = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        errorResponse.ShouldNotBeNull();
        errorResponse.Title.ShouldNotBeNull();
        errorResponse.Title.ShouldContain("One or more validation errors occurred.");
    }
    
    [Fact]
    public async Task GetWeatherInfo_InvalidCity_Failure()
    {
        // Arrange
        const string city = CustomWebApplicationFactory.NotExistingCity;
        
        // Act
        var response = await _client.GetAsync($"weatherinfo?city={city}");

        // Assert
        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var errorResponse = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        errorResponse.ShouldNotBeNull();
        errorResponse.Title.ShouldNotBeNull();
        errorResponse.Title.ShouldContain("No matching location found.");
    }
    
    [Fact]
    public async Task GetWeatherInfo_ValidCity_Success()
    {
        // Arrange
        const string city = CustomWebApplicationFactory.ExistingCity;

        // Act
        var response = await _client.GetAsync($"weatherinfo?city={city}");

        // Assert
        response.ShouldNotBeNull();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var weatherInfoResponse = await response.Content.ReadFromJsonAsync<WeatherInfoResponse>();
        weatherInfoResponse.ShouldNotBeNull();
    }
}