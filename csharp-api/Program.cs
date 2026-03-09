var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddSingleton<IStockAnalysisService, StockAnalysisService>();
builder.Services.AddSingleton<IPalindromeService, PalindromeService>();
builder.Services.AddSingleton<ITwoSumService, TwoSumService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("/weatherforecast", (IWeatherForecastService weatherForecastService) =>
{
    var forecast = weatherForecastService.GetForecast();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapPost("/besttimetobyorsellstock", (BestTimeToBuyOrSellStockRequest request, IStockAnalysisService stockAnalysisService) =>
{
    var prices = request.Prices;

    if (prices == null || prices.Length < 2)
    {
        return Results.BadRequest("Please provide at least two stock prices.");
    }

    var maxProfit = stockAnalysisService.GetMaxProfit(prices);
    return Results.Ok(maxProfit);
}).WithName("GetBestTimeToBuyOrSellStock");

app.MapPost("/longestpalidromicsubstring", (LongestPalidromicSubstringRequest request, IPalindromeService palindromeService) =>
{
    var s = request.S;

    if (string.IsNullOrEmpty(s))
    {
        return Results.BadRequest("Please provide a non-empty string.");
    }

    var sReturn = palindromeService.GetLongestPalindromicSubstring(s);
    return Results.Ok(sReturn);
}).WithName("GetLongestPalindromicSubstring");

app.MapPost("/twosum", (TwoSumRequest request, ITwoSumService twoSumService) =>
{
    var nums = request.Nums;
    var target = request.Target;

    if (nums == null || nums.Length < 2)
    {
        return Results.BadRequest("Please provide at least two numbers.");
    }

    if (twoSumService.TryFindPair(nums, target, out var pair))
    {
        return Results.Ok(pair);
    }

    return Results.BadRequest("No two sum solution found.");
}).WithName("GetTwoSum");

app.Run();

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record BestTimeToBuyOrSellStockRequest(int[] Prices);

record LongestPalidromicSubstringRequest(string S);

record TwoSumRequest(int[] Nums, int Target);

public partial class Program;
