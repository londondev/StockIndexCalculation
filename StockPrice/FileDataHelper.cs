using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using StockPriceService.Messages;

namespace StockPrice
{
    public static class FileDataHelper
    {
        public static async Task<FileData> GetFileDataAsync(HttpRequestMessage message)
        {
            var provider = new MultipartMemoryStreamProvider();

            await message.Content.ReadAsMultipartAsync(provider);

            var content = provider.Contents.Single();
            var stream = await content.ReadAsStreamAsync();
            return new FileData
            {
                FileName = content.Headers.ContentDisposition.FileName.Replace("\"", ""),
                FileContent = stream
            };
        }

        
    }
}