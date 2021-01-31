using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aspcore_watchshop.Models;

namespace aspcore_watchshop.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public CartController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SumbitOrder()
        {
            return View();
        }

        public IActionResult OrderStatus()
        {
            ViewBag.PageTitle = "OrderStatus";
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
