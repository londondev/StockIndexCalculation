using System;
using StockPrice.Data;

namespace StockPriceService.Messages
{
    public class WeightedStockData 
    {
        public string IndexName { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Share { get; set; }
        public decimal Weight { get; set; }
    }
}