using StockMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModels
{
    public class TableViewModels
    {
        public IEnumerable<IDictionary<string, string>> Records { get; set; }

        public IEnumerable<string> Keys { get; set; }

        public string StockName { get; set; }
    }
}
