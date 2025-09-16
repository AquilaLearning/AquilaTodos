namespace AquilaTodos.Tests.Weather;

using AquilaTodos.Weather;
using Shouldly;

public class WeatherApplicationFactory : TestApplicationFactory
{
    protected override void SeedData(TodoContext context)
    {
        // No seeding required for weather tests
    }
}

public class WeatherControllerTests : IClassFixture<WeatherApplicationFactory>
{
    private readonly WeatherApplicationFactory factory;

    public WeatherControllerTests(WeatherApplicationFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Should_return_weather_for_city()
    {
        using var client = this.factory.CreateClient();
        var result = await client.GetObjectAsync<WeatherDto>("api/weather/London");

        result.ShouldNotBeNull();
    }
}
