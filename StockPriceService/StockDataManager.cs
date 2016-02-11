using System;
using System.Collections.Generic;
using System.Linq;
using StockPrice.Data;
using StockPriceService.Messages;

namespace StockPriceService
{
    public class StockDataManager : IStockDataManager
    {
        //TODO: Move this to the config file
        const int INITIAL_INDEX_LEVEL = 100;

        private IFileDataParser _fileDataParser;

        public StockDataManager(IFileDataParser fileDataParser)
        {
            _fileDataParser=fileDataParser;
        }
        public IndexData CalculateIndex(FileData fileData)
        {
            var stockData= _fileDataParser.GetStockData(fileData.FileContent).ToList();

            SaveData(stockData);

            return new IndexData
            {
                Indexes = CalculateIndexes(stockData),
                StockWeights = CalculateStockWeights(stockData),
                WeightedStockData = CalculateWeightedStockData(stockData)
            };
        }

        private static void SaveData(List<Stock> stockData)
        {
            try
            {
                var context = new StockContext();
                context.Stocks.AddRange(stockData);
                context.SaveChangesAsync();
            }
            catch (Exception)
            {
                //I am not allowing to throw exception as I don't want to stop 
                //demo working due to any unexpected db issue
                
                //TODO: Remove try,catch block here and catch exception at api level properly.
            }
        }

        private List<WeightedStockData> CalculateWeightedStockData(List<Stock> stockDatas)
        {
            List<WeightedStockData> weightedStockDataList=new List<WeightedStockData>();
            var allDates = GetAllDates(stockDatas);
            foreach (var date in allDates)
            {
               var weightedDayStockList= stockDatas.Where(s => s.Date == date).Select(s => new WeightedStockData
                {
                    Date = s.Date,
                    Share = s.Share,
                    Price = s.Price,
                    Id = s.StockId,
                    IndexName = s.IndexName,
                    Name = s.Name,
                    Weight = Math.Round(s.Price*s.Share*100/stockDatas.Where(ss => ss.Date == s.Date).Sum(ss => ss.Price*ss.Share),3)
                }).ToList();

               weightedStockDataList.AddRange(weightedDayStockList);
            }

            return weightedStockDataList;
        }

        private IList<StockWeight> CalculateStockWeights(IList<Stock> stockDatas)
        {
            var lastDate = stockDatas.Select(a => a.Date).OrderByDescending(a => a.Date).First();
            return stockDatas.Where(s=>s.Date==lastDate).Select(a => new StockWeight
            {
                StockId = a.StockId,
                Weight = Math.Round(a.Price * a.Share * 100 / stockDatas.Where(ss => ss.Date == lastDate).Sum(ss => ss.Price * ss.Share), 3)
            }).ToList().OrderBy(a=>a.Weight).Take(5).ToList();
        }

        private IList<IndexDate> CalculateIndexes(IList<Stock> stockDatas)
        {
            IList<IndexDate> indexDateList=new List<IndexDate>();
            var allDates = GetAllDates(stockDatas);

            var firstDayIndexValue = GetIndexValue(stockDatas, allDates.First());
            foreach (var date in allDates)
            {
                var currentDateValue = GetIndexValue(stockDatas, date);
                indexDateList.Add(new IndexDate
                {
                     Date = date.ToString("MMM-dd"),
                     Index = Math.Round(currentDateValue * INITIAL_INDEX_LEVEL / firstDayIndexValue,3)
                });
            }
            return indexDateList;
        }

        private static List<DateTime> GetAllDates(IList<Stock> stockDatas)
        {
            return stockDatas.Select(a => a.Date).OrderBy(a => a.Date).Distinct().ToList();
        }

        private static decimal GetIndexValue(IEnumerable<Stock> stockDatas, DateTime date)
        {
            return stockDatas.Where(a => a.Date == date).Sum(a => a.Price*a.Share);
        }
    }
}
