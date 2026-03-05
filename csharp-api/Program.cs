var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapPost("/besttimetobyorsellstock", (BestTimeToBuyOrSellStockRequest request) =>
{
    var prices = request.Prices;

    if (prices == null || prices.Length < 2)
    {
        return Results.BadRequest("Please provide at least two stock prices.");
    }

    int maxProfit = 0;
    int minPrice = int.MaxValue;
    minPrice = minPrice > prices[0] ? prices[0] : minPrice;

    for (int i = 1; i < prices.Length; i++)
    {
        minPrice = minPrice > prices[i] ? prices[i] : minPrice;
        maxProfit = prices[i] - minPrice > maxProfit ? prices[i] - minPrice : maxProfit;
    }

    return Results.Ok(maxProfit);
}).WithName("GetBestTimeToBuyOrSellStock");

app.MapPost("/longestpalidromicsubstring", (LongestPalidromicSubstringRequest request) =>
{
    var s = request.S;

    if (string.IsNullOrEmpty(s))
    {
        return Results.BadRequest("Please provide a non-empty string.");
    }

    string sReturn = "";
    string temp = "";
    for(int i = 0; i < s.Length; i++){

        temp = ExpandCenter(i, i, s);

        if(sReturn.Length < temp.Length)
            sReturn = temp;

        temp = ExpandCenter(i, i + 1, s);

        if(sReturn.Length < temp.Length)
            sReturn = temp;
    }

    return Results.Ok(sReturn);
}).WithName("GetLongestPalindromicSubstring");

static string ExpandCenter(int left, int right, string s)
{
    while (left >= 0 && right < s.Length && s[left] == s[right])
    {
        left--;
        right++;
    }
    return s.Substring(left + 1, right - left - 1);
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

record BestTimeToBuyOrSellStockRequest(int[] Prices);

record LongestPalidromicSubstringRequest(string S);

public partial class Program;
