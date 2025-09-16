namespace AquilaTodos.Weather;

public record WeatherDto
{
    public required string Temperature { get; init; }
    public required string Wind { get; init; }
    public required string Description { get; init; }
}
