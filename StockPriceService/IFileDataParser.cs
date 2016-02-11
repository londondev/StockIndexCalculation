using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPrice.Data;
using StockPriceService.Messages;

namespace StockPriceService
{
    public interface IFileDataParser
    {
        IEnumerable<Stock> GetStockData(Stream fileContent);
    }
}
