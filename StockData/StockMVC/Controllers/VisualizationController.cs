using Microsoft.AspNet.Mvc;
using StockMVC.Services;
using StockMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Controllers
{
    public class VisualizationController : Controller
    {
        private IStockData _stockDate;

        public VisualizationController(IStockData stockDate)
        {
            _stockDate = stockDate;
        }

        public async Task<IActionResult> Index(string code, DateTime from, DateTime to)
        {
            var stockNames = code.Split('+');

            var allRecords = await _stockDate.Get(stockNames, from, to);

            var model = new StockDictionaryViewModels { StockNames = stockNames, Records = allRecords };

            model.Keys = allRecords[stockNames[0]].ToList()[0].Keys;

            return View(model);
        }
    }
}
