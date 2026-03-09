using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace c_sharp_api.Tests;

public class EndpointsTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public EndpointsTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost")
        });
    }

    [Fact]
    public async Task BestTimeToBuyOrSellStock_ReturnsMaxProfit_FromJsonBody()
    {
        var response = await _client.PostAsJsonAsync("/besttimetobyorsellstock", new
        {
            prices = new[] { 7, 1, 5, 3, 6, 4 }
        });

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<int>();

        Assert.Equal(5, result);
    }

    [Fact]
    public async Task BestTimeToBuyOrSellStock_ReturnsBadRequest_WhenLessThanTwoPrices()
    {
        var response = await _client.PostAsJsonAsync("/besttimetobyorsellstock", new
        {
            prices = new[] { 7 }
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task LongestPalidromicSubstring_ReturnsResult_FromJsonBody()
    {
        var response = await _client.PostAsJsonAsync("/longestpalidromicsubstring", new
        {
            s = "forgeeksskeegfor"
        });

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<string>();

        Assert.Equal("geeksskeeg", result);
    }

    [Fact]
    public async Task LongestPalidromicSubstring_ReturnsBadRequest_WhenEmptyInput()
    {
        var response = await _client.PostAsJsonAsync("/longestpalidromicsubstring", new
        {
            s = ""
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task TwoSum_ReturnsBadRequest_WhenLessThanTwoNumbers()
    {
        var response = await _client.PostAsJsonAsync("/twosum", new
        {
            nums = new[] { 7 },
            target = 10
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task TwoSum_ReturnsMaxResult_FromJsonBody()
    {
        var response = await _client.PostAsJsonAsync("/twosum", new
        {
            nums = new[] { 7, 1, 5, 3, 6, 4 },
            target = 10
        });

        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<int[]>();

        Assert.Equal(new[] { 0, 3 }, result);
    }
}