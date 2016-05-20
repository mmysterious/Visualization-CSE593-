using System;
using System.Collections;
using System.Collections.Generic;

namespace StockMVC.Services
{
    public interface IStockData
    {
        IEnumerable<IDictionary<string, string>> Get(string name, DateTime from, DateTime to);
    }
}