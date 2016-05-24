using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockMVC.Services
{
    public interface IStockData
    {
        Task<IEnumerable<IDictionary<string, string>>>
            Get(string name, DateTime from, DateTime to);

        Task<IDictionary<string, IEnumerable<IDictionary<string, string>>>>
            Get(string[] names, DateTime from, DateTime to);
    }
}