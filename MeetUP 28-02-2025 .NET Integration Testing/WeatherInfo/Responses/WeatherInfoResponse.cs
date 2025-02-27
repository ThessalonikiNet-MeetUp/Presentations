
namespace WeatherInfo.Responses;

public record WeatherInfoResponse(Location Location, Details Current);

public record Location(string Name, string Region, string Country);

public record Details(double TempC, double TempF);

public record ErrorResponse(ErrorResponseDetails Error);

public record ErrorResponseDetails(int Code, string Message);
