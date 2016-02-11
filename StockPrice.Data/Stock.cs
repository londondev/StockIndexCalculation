using System;

namespace StockPrice.Data
{
    public class Stock
    {
        public int Id { get; set; }
        public string IndexName { get; set; }
        public string Name { get; set; }
        public string StockId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public int Share { get; set; }
    }
}
