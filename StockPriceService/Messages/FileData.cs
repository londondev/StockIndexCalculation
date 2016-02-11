using System.IO;

namespace StockPriceService.Messages
{
    public class FileData
    {
        public string FileName { get; set; }
        public Stream FileContent { get; set; }
    }
}