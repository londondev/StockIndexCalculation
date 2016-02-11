using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockPriceService;

namespace StockPrice.Tests
{
    /// <summary>
    /// Summary description for CsvDataParserTests
    /// </summary>
    [TestClass]
    public class CsvDataParserTests
    {
        private CsvDataParser csvDataParser;
   
        [TestInitialize]
        public void Initialize()
        {
            csvDataParser=new CsvDataParser();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: Add test logic here
            //
        }

        private Stream GetTestData(int numberOfStock)
        {
            string header = "INDEX_NAME,DATE,STOCK_ID,NAME,PRICE,NUM_SHARES";
            string sampleLine = "FTRND,2015-01-{0},98OLSGD{0},StockA{0},199{0},617{0}";
            List<string> testDataList=new List<string>();
            for (int i = 1; i <= numberOfStock; i++)
            {
                testDataList.Add(string.Format(sampleLine,i.ToString()));
            }

            return testDataList.to
        }

    }
}
