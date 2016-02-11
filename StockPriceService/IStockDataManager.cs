using StockPriceService.Messages;

namespace StockPriceService
{
    public interface IStockDataManager
    {
        IndexData CalculateIndex(FileData fileData);
    }
}
