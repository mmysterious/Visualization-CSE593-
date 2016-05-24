using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.ViewModels
{
    public class StockDictionaryViewModels
    {
        public IDictionary<string, IEnumerable<IDictionary<string, string>>> Records { get; set; }

        public IEnumerable<string> Keys { get; set; }

        public IEnumerable<string> StockNames { get; set; }
    }
}
