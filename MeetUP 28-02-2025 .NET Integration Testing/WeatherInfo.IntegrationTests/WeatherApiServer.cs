using System.Net;
using System.Text.Json;
using WeatherInfo.Responses;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace WeatherInfo.IntegrationTests;

public class WeatherApiServer: IDisposable
{
    private WireMockServer _server;
    
    public string Url => _server.Url!;
    
    public void Start()
    {
        _server = WireMockServer.Start();
    }
    
    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
    
    public void SetupExistingCity(string city)
    {
        var response = new WeatherInfoResponse(new Location(city, "Central Macedonia", "Greece"), new Details(20.0, 68.0));
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        
        _server.Given(Request.Create()
                .WithPath("/")
                .WithParam("q", city)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(JsonSerializer.Serialize(response, serializerOptions))
                .WithStatusCode(HttpStatusCode.OK));
    }
    
    public void SetupNotExistingCity(string city)
    {
        var response = new ErrorResponse(new ErrorResponseDetails(1006, "No matching location found."));
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };
        
        _server.Given(Request.Create()
                .WithPath("/")
                .WithParam("q", city)
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(JsonSerializer.Serialize(response, serializerOptions))
                .WithStatusCode(HttpStatusCode.BadRequest));
    }
}