﻿using Microsoft.AspNet.Mvc;
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
            var stockNames = code.Split('+');

            var allRecords = await _stockData.Get(stockNames, from, to);

            var model = new StockDictionaryViewModels { StockNames = stockNames, Records = allRecords };

            model.Keys = allRecords[stockNames[0]].ToList()[0].Keys;

            return View(model);
        }
    }
}
