using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockData.Models
{
    public class FinanceDBContext: DbContext
    {
        public FinanceDBContext() : base("StockData")
        {

        }

        public DbSet<Data> Records { get; set; }
    }
}