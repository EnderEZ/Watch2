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
    public class ProductController : Controller
    {
        public static List<Product> db = new List<Product>()
            {
                new Product(){ID=1,Name="Ролекс", Price=200,Discount=0, CateID=1, WireID=1, SaleCount=100,Image="img.jpg"},
                new Product(){ID=2,Name="Ролекс", Price=1000,Discount=0, CateID=1, WireID=2, SaleCount=99,Image="img.jpg"},
                new Product(){ID=3,Name="Ролекс", Price=2000,Discount=0, CateID=1, WireID=3, SaleCount=120,Image="img.jpg"},
                new Product(){ID=4,Name="Ролекс", Price=19000,Discount=0, CateID=1, WireID=1, SaleCount=10,Image="img2.jpg"},
                new Product(){ID=5,Name="Ролекс", Price=500,Discount=0, CateID=1, WireID=1, SaleCount=0,Image="img3.jpg"},
                new Product(){ID=6,Name="Ролекс", Price=300,Discount=0, CateID=2, WireID=2, SaleCount=12,Image="img3.jpg"},
                new Product(){ID=7,Name="Ролекс", Price=646,Discount=0, CateID=2, WireID=3, SaleCount=85,Image="img3.jpg"},
                new Product(){ID=8,Name="Ролекс", Price=567,Discount=0, CateID=2, WireID=1, SaleCount=160,Image="img2.jpg"},
                new Product(){ID=9,Name="Ролекс", Price=7675,Discount=0, CateID=2, WireID=1, SaleCount=85,Image="img2.jpg"},
                new Product(){ID=10,Name="Ролекс", Price=8567,Discount=0, CateID=2, WireID=3, SaleCount=45,Image="img.jpg"},
                new Product(){ID=11,Name="Ролекс", Price=5665,Discount=0, CateID=2, WireID=1, SaleCount=58,Image="img.jpg"},
                new Product(){ID=12,Name="Ролекс", Price=3445,Discount=0, CateID=3, WireID=2, SaleCount=65,Image="img.jpg"},
                new Product(){ID=13,Name="Ролекс", Price=667,Discount=0, CateID=3, WireID=2, SaleCount=78,Image="img.jpg"},
                new Product(){ID=14,Name="Ролекс", Price=820,Discount=0, CateID=3, WireID=1, SaleCount=23,Image="img2.jpg"},
                new Product(){ID=15,Name="Ролекс", Price=577,Discount=0, CateID=3, WireID=2, SaleCount=85,Image="img2.jpg"},
                new Product(){ID=16,Name="Ролекс", Price=720,Discount=0, CateID=3, WireID=3, SaleCount=89,Image="img.jpg"},
                new Product(){ID=17,Name="Ролекс", Price=350,Discount=0, CateID=3, WireID=3, SaleCount=15,Image="img.jpg"},
                new Product(){ID=18,Name="Ролекс", Price=400,Discount=0, CateID=3, WireID=3, SaleCount=14,Image="img2.jpg"}
            };

        private readonly ILogger<HomeController> _logger;

        public ProductController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ActionName("Discount")]
        public IActionResult Discount()
        {
            ViewBag.PageTitle = "Discount";
            ViewBag.PageCode = 0;
            return View("Index");
        }

        [ActionName("Men")]
        public IActionResult Men()
        {
            ViewBag.PageTitle = "Men";
            ViewBag.PageCode = 1;
            return View("Index");
        }

        [ActionName("Women")]
        public IActionResult Women()
        {
            ViewBag.PageTitle = "Women";
            ViewBag.PageCode = 2;
            return View("Index");
        }

        [ActionName("Couple")]
        public IActionResult Couple()
        {
            ViewBag.PageTitle = "Couple";
            ViewBag.PageCode = 3;
            return View("Index");
        }

        [ActionName("Accessories")]
        public IActionResult Accessories()
        {
            ViewBag.PageTitle = "Accessories";
            ViewBag.PageCode = 4;
            return View("Index");
        }

        [ActionName("FindProductByChart")]
        public IActionResult FindProductByChart(string text)
        {
            ViewBag.PageTitle = "FindProductByChart";
            ViewBag.PageCode = -1;
            ViewBag.SearchResult = 50;
            string result = "";
            db.ForEach(item =>
            {
                if (item.Name.Contains(text)) result += item.ID + ",";
            });
            TempData["result"] = result;
            return View("Index");
        }

        public IActionResult Detail(int id)
        {
            Product product = db.FirstOrDefault(w => w.ID == id);

            return View(product);
        }

        //AJAX
        public JsonResult ProductByCate(int pageCode, int number)
        {
            if (number != 0) return Json(db.Take(number));
            if (pageCode == -1) return Json(GetProductsByID(TempData["result"] as string)); // get products form list search result
            if (pageCode == -2) return Json(db.Take(number)); // get products best seller
            return Json(db);
        }

        public JsonResult ProductInCart(string orderItemID)
        {
            string[] itemIDs = orderItemID.Split(',');
            List<Product> reponse = new List<Product>();
            foreach (var id in itemIDs)
            {
                if (id == "") break;
                Product obj = db.Find(p => p.ID == Int32.Parse(id));
                if (obj == null) break;
                reponse.Add(obj);
            }
            return Json(reponse);
        }


        public List<Product> GetProductsByID(string str)
        {
            List<Product> ls = new List<Product>();
            string[] arr = str.Split(',');
            foreach (string id in arr)
            {
                if (id != "")
                {
                    int i = Int32.Parse(id);
                    ls.Add(db.Find(item => item.ID == i));
                }
            }
            return ls;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
