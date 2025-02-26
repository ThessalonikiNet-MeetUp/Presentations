using System.Net;
using Microsoft.AspNetCore.Mvc;
using WeatherInfo.Responses;
using WeatherInfo.Services;

namespace WeatherInfo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherInfoController(IWeatherService weatherService) : ControllerBase
{

    [HttpGet(Name = "GetWeatherInfo")]
    public async Task<ActionResult<WeatherInfoResponse>> Get(string city)
    {
        try
        {
            return await weatherService.GetWeatherInfo(city);
        }
        catch (Exception e)
        {
            return Problem(title: e.Message, 
                detail: $"Code: {e.Data["Code"]}", 
                statusCode: (int)HttpStatusCode.BadRequest);
        }
    }
}