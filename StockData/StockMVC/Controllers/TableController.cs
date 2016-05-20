using Microsoft.AspNet.Mvc;
using StockMVC.Services;
using StockMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Controllers
{
    [Route("[controller]/[action]")]
    public class TableController : Controller
    {
        private IStockData _stockData;

        public TableController(IStockData stockData)
        {
            _stockData = stockData;
        }

        public async Task<IActionResult> Index(string code, DateTime from, DateTime to)
        {
            var records = await _stockData.Get(code, from, to);

            var model = new TableViewModels { StockName = code, Records = records };

            if (records.ToList().Count == 0)
                model.Keys = new string[0];
            else
                model.Keys = records.ToList()[0].Keys;

            return View(model);
        }
    }
}
