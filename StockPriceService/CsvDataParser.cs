using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPrice.Data;
using StockPriceService.Messages;

namespace StockPriceService
{
    public class CsvDataParser : IFileDataParser
    {
        public IEnumerable<Stock> GetStockData(Stream fileContent)
        {
            List<string> contentLines = GetLinesFromStream(fileContent);
            //INDEX_NAME,DATE,      STOCK_ID,NAME,  PRICE,NUM_SHARES
            //FTRND,     2015-01-01,98OLSGD, StockA,1990, 6179
         contentLines.RemoveAt(0); //Remove header   
         return contentLines.Select(a => a.Split(',')).Select(s => new Stock
                {
                    IndexName = s[0],
                    Name=s[3],
                    StockId = s[2],
                    Date = Convert.ToDateTime(s[1], CultureInfo.CurrentCulture),
                    Price = Convert.ToDecimal(s[4]),
                    Share = Convert.ToInt32(s[5])
                });
        }

        private static List<string> GetLinesFromStream(Stream stream)
        {
            var lines = new List<string>();
            using (var streamReader = new StreamReader(stream))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
