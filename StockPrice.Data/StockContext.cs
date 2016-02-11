using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockPrice.Data;

namespace StockPrice.Data
{
    public class StockContext:DbContext
    {

        public StockContext()
            : base("name=StockDb") 
        {
            Database.SetInitializer(new StockDbInitializer());
        }
        public DbSet<Stock> Stocks { get;set; }
    }


}

public class StockDbInitializer : DropCreateDatabaseAlways<StockContext>
{
     
}
