using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using StockPriceService;

namespace StockPrice.Controllers
{
    public class IndexDataController : ApiController
    {
        private IStockDataManager _stockDataManager;

        public IndexDataController(IStockDataManager stockDataManager)
        {
            //TODO: Install DI framework and don't use "new"
            _stockDataManager = stockDataManager;
        }


        public async Task<IHttpActionResult> Post()
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            try
            {
                var fileData = await FileDataHelper.GetFileDataAsync(Request);

                var indexData=_stockDataManager.CalculateIndex(fileData);


                return Ok(new
                {
                    message = "Success", 
                    plotChartData = indexData.Indexes,
                    pieChartData=indexData.StockWeights,
                    dataTableData=indexData.WeightedStockData
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }
    }

    public class AddStockRequest
    {
        public string FileName { get; set; }
        public string Data { get; set; }
    }
}
