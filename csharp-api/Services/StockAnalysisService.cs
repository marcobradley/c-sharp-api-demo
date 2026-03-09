public class StockAnalysisService : IStockAnalysisService
{
    public int GetMaxProfit(int[] prices)
    {
        var maxProfit = 0;
        var minPrice = prices[0];

        for (var i = 1; i < prices.Length; i++)
        {
            minPrice = minPrice > prices[i] ? prices[i] : minPrice;
            maxProfit = prices[i] - minPrice > maxProfit ? prices[i] - minPrice : maxProfit;
        }

        return maxProfit;
    }
}
