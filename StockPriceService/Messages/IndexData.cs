using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPriceService.Messages
{
    public class IndexData
    {
        public IList<IndexDate> Indexes { get; set; }
        public IList<StockWeight> StockWeights { get; set; }
        public List<WeightedStockData> WeightedStockData { get; set; } 
    }

    public class IndexDate
    {
        public string Date { get; set; }
        public Decimal Index { get; set; }
    }

    public class StockWeight
    {
        public string StockId { get; set; }
        public Decimal Weight { get; set; }
    }
}
