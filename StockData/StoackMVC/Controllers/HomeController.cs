using Microsoft.AspNet.Mvc;
using StockMVC.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Query model)
        {
            return RedirectToAction("index", "table",
                new { code = model.Code, from = model.From, to = model.To });
        }
    }
}
