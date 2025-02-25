namespace WeatherInfo.Entities;

public class WeatherSearchEntry
{
    public int Id { get; set; }

    public required string LocationName { get; set; }

    public DateTime OccuredAt { get; set; }

    public bool IsSuccess { get; set; }  
}